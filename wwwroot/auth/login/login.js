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
  window.location.href = 'index.html';
} else {
  const errorText = await res.text(); // tenta ler o corpo da resposta
  console.error('Erro no login:', errorText); // log no console

  const erroLogin = document.getElementById('erroLogin');
  erroLogin.style.display = 'block';
  erroLogin.innerText = 'Erro ao fazer login: ' + errorText;
}
});
