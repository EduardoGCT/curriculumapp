document.getElementById('btn').addEventListener('click', async () => {
    const valorInput = document.getElementById('name').value;

    // 1. Correção dos parênteses no trim()
    if (!valorInput.trim()) {
        alert('Por favor, digite algo antes de enviar.');
        return;
    }

    const dados = {
        name: valorInput
    };

    try {
        const response = await fetch('http://127.0.0.1:5195/curriculum', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(dados)
        });

        const textoBruto = await response.text();
        console.log('Resposta bruta do servidor:', textoBruto);

        let resultado;
        try {
            resultado = JSON.parse(textoBruto);
            console.log('Sucesso (JSON):', resultado);
        } catch (e) {
            console.log('O servidor não respondeu um JSON, mas respondeu texto:', textoBruto);
            resultado = textoBruto;
        }

        alert('Processado com sucesso!');
        
    } catch (erro) {
        console.error('Erro na requisição:', erro);
        alert('Erro ao enviar os dados.');
    }
});
