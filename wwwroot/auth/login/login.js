document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('loginForm').addEventListener('submit', async function (e) {
        e.preventDefault();

        const email = document.getElementById('email').value;
        const senha = document.getElementById('senha').value;

        const res = await fetch('https://localhost:7047/api/auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ nome: '', email: email, senhaHash: senha })
        });

        if (res.ok) {
            const data = await res.json();
            localStorage.setItem('token', data.token);
            localStorage.setItem('userId', data.userId);
            window.location.href = '/index.html'; // <-- Redireciona para a página correta na raiz
        } else {
            const errorText = await res.text();
            console.error('Erro no login:', errorText);

            const erroLogin = document.getElementById('erroLogin');
            erroLogin.style.display = 'block';
            erroLogin.innerText = 'Erro ao fazer login: ' + errorText;
        }
    });
});
