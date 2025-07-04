const apiUrl = 'https://localhost:7047/dividas'; // Altere se precisar

document.addEventListener('DOMContentLoaded', loadDividas);
document.getElementById('dividaForm').addEventListener('submit', addDivida);

async function loadDividas() {
    const res = await fetch(apiUrl);
    const dividas = await res.json();

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
        `;
        tbody.appendChild(tr);
        totalMes += d.valorParcela;
    });

    document.getElementById('totalParcelasValor').textContent = `R$ ${totalMes.toFixed(2)}`;
}

async function addDivida(e) {
    e.preventDefault();

    const divida = {
        nome: document.getElementById('nome').value,
        origem: document.getElementById('origem').value,
        valorTotal: parseFloat(document.getElementById('valorTotal').value),
        totalParcelas: parseInt(document.getElementById('totalParcelas').value),
        parcelaAtual: parseInt(document.getElementById('parcelaAtual').value),
        valorParcela: parseFloat(document.getElementById('valorParcela').value),
        dataPagamento: document.getElementById('dataPagamento').value,
        proximoVencimento: document.getElementById('proximoVencimento').value
    };

    try {
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(divida)
        });

        if (!response.ok) {
            throw new Error(`Erro ao salvar: ${response.statusText}`);
        }

        e.target.reset();
        loadDividas();
    } catch (error) {
        alert(`Falha ao adicionar dívida: ${error.message}`);
        console.error(error);
    }
}


function formatarData(dataString) {
    const data = new Date(dataString);
    return data.toLocaleDateString('pt-BR', { timeZone: 'UTC' });
}
