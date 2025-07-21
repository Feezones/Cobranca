document.getElementById('registerForm').addEventListener('submit', async function (e) {
    e.preventDefault();

    const nome = document.getElementById('nome').value;
    const email = document.getElementById('email').value;
    const senha = document.getElementById('senha').value;

    try {
        const res = await fetch('https://localhost:7047/api/auth/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ nome, email, senha })
        });

        if (res.ok) {
            document.getElementById('sucessoCadastro').style.display = 'block';
            document.getElementById('erroCadastro').style.display = 'none';
        } else {
            const erro = await res.json();
            document.getElementById('erroCadastro').innerText = erro?.title || 'Erro ao cadastrar.';
            document.getElementById('erroCadastro').style.display = 'block';
            document.getElementById('sucessoCadastro').style.display = 'none';
        }
    } catch (err) {
        document.getElementById('erroCadastro').innerText = 'Erro na comunicação com o servidor.';
        document.getElementById('erroCadastro').style.display = 'block';
    }
});
