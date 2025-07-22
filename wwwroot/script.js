if (!localStorage.getItem('token')) {
    window.location.href = 'auth/login/login.html';
}


const apiUrl = 'https://localhost:7047/api/dividas'; // Altere se precisar

document.addEventListener('DOMContentLoaded', loadDividas);
document.getElementById('dividaForm').addEventListener('submit', saveDivida);

var dividas;


async function loadDividas() {

    const res = await fetch(`${apiUrl}`, {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer ' + localStorage.getItem('token'),
            'Content-Type': 'application/json',
        }
    });

    dividas = await res.json();


    const tbody = document.querySelector('#dividasTable tbody');
    tbody.innerHTML = '';

    let totalMes = 0;

    dividas.forEach(d => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
        <td>${d.nome}</td>
        <td>${d.origem}</td>
        <td>R$ ${d.valorTotal.toFixed(2)}</td>
        <td>${d.totalParcelas}</td>
        <td>${d.parcelaAtual}</td>
        <td>R$ ${d.valorParcela.toFixed(2)}</td>
        <td>${formatarData(d.dataPagamento)}</td>
        <td>${formatarData(d.proximoVencimento)}</td>
        <td>
            <button class="btn btn-sm btn-warning me-2" onclick="editarDivida(${d.id})">Editar</button>
            <button class="btn btn-sm btn-danger" onclick="deletarDivida(${d.id})">Excluir</button>
        </td>
    `;
        tbody.appendChild(tr);
        totalMes += d.valorParcela;
    });


    document.getElementById('totalParcelasValor').textContent = `R$ ${totalMes.toFixed(2)}`;
}

async function saveDivida(e) {
    e.preventDefault();

    const divida = {
        nome: document.getElementById('nome').value,
        origem: document.getElementById('origem').value,
        valorTotal: parseFloat(document.getElementById('valorTotal').value),
        totalParcelas: parseInt(document.getElementById('totalParcelas').value),
        parcelaAtual: parseInt(document.getElementById('parcelaAtual').value),
        valorParcela: parseFloat(document.getElementById('valorParcela').value),
        dataPagamento: document.getElementById('dataPagamento').value,
        proximoVencimento: document.getElementById('proximoVencimento').value,
        userId: localStorage.getItem('userId') // pega do localStorage
    };


    try {
        let response;
        if (editandoId) {
            response = await fetch(`${apiUrl}/${editandoId}`, {
                method: 'PUT',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('token'),
                    'Content-Type': 'application/json'
                }
                ,
                body: JSON.stringify(divida)
            });
        } else {
            response = await fetch(apiUrl, {
                method: 'POST',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('token'),
                    'Content-Type': 'application/json'
                }
                ,
                body: JSON.stringify(divida)
            });
        }

        if (!response.ok) throw new Error('Erro ao salvar');

        e.target.reset();
        editandoId = null;
        loadDividas();
    } catch (error) {
        alert(`Erro ao salvar dívida: ${error.message}`);
        console.error(error);
    }
}


async function deletarDivida(id) {
    if (!confirm('Tem certeza que deseja excluir esta dívida?')) return;

    try {
        const res = await fetch(`${apiUrl}/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token'),
                'Content-Type': 'application/json'
            }
        });
        if (!res.ok) throw new Error('Erro ao excluir');
        loadDividas();
    } catch (error) {
        alert(`Erro ao excluir dívida: ${error.message}`);
        console.error(error);
    }
}

let editandoId = null;

async function editarDivida(id) {
    try {
        const divida = dividas.find(d => d.id === id);

        if (!divida) return alert('Dívida não encontrada.');

        // Preenche o formulário
        document.getElementById('nome').value = divida.nome;
        document.getElementById('origem').value = divida.origem;
        document.getElementById('valorTotal').value = divida.valorTotal;
        document.getElementById('totalParcelas').value = divida.totalParcelas;
        document.getElementById('parcelaAtual').value = divida.parcelaAtual;
        document.getElementById('valorParcela').value = divida.valorParcela;
        document.getElementById('dataPagamento').value = divida.dataPagamento.slice(0, 10);
        document.getElementById('proximoVencimento').value = divida.proximoVencimento.slice(0, 10);

        editandoId = id;
    } catch (error) {
        alert('Erro ao carregar dívida.');
        console.error(error);
    }
}



function formatarData(dataString) {
    const data = new Date(dataString);
    return data.toLocaleDateString('pt-BR', { timeZone: 'UTC' });
}


function logout() {
    localStorage.removeItem('token');
    window.location.href = 'login.html';
}

