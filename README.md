#  Integração entre ItemService e RestauranteService para o consumo de mensagens com RabbitMQ 

Este projeto é realizado a integração entre o ItemService e o RestauranteService utilizando RabbitMQ como o serviço de mensageria para a comunicação assíncrona entre os serviços.
Foi desenvolvido com o framework .NET Core e realizada a criação de images para a utilização em containers com Docker.

### RabbitMQ:
![RabbitMQ](https://github.com/Jonas-Emir/microsservicos_restauranteService/blob/ImplementacaoRabbitMQ/rabbit.PNG?raw=true)

### Endpoints de ItemService e RestauranteService:
![ItemService](https://github.com/Jonas-Emir/microsservicos_restauranteService/blob/ImplementacaoRabbitMQ/itemservice.PNG?raw=true)
![RestauranteService](https://github.com/Jonas-Emir/microsservicos_restauranteService/blob/ImplementacaoRabbitMQ/restauranteservice.PNG?raw=true)

### Containers:
![Containers](https://github.com/Jonas-Emir/microsservicos_restauranteService/assets/89087399/8414af30-68d0-4a99-b64b-922f74f5220f)

### Images:
![Images](https://github.com/Jonas-Emir/microsservicos_restauranteService/blob/ImplementacaoRabbitMQ/dockerps.PNG?raw=true)


## Configuração do Projeto

1. Clone este repositório:

   ```bash
   git clone https://github.com/Jonas-Emir/microsservicos_restauranteService.git

2. Navegue até o diretório onde está o Dockerfile do ItemService.
Execute o seguinte comando para construir a imagem do ItemService, coloque a versão desejada:
   ```bash
    docker build -t itemservice:1.0 .


3. Navegue até o diretório onde está o Dockerfile do RestauranteService.
Execute o seguinte comando para construir a imagem do RestauranteService, coloque a versão desejada:
    ```bash
   docker build -t restauranteservice:1.0 .

4. Inicialização dos Serviços de Banco de Dados:
   ```bash
   docker run --name sql1 -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Dev123!' -d --network restaurante-bridge mcr.microsoft.com/mssql/server:latest

5. Inicialização RabbitMQ:
   ```bash
    docker run -d --hostname rabbitmq-service --name rabbitmq-service --network restaurante-bridge -p 5672:5672 rabbitmq:3-management

6. Run Container RestauranteService:
   ```bash
   docker run --name restauranteservice -p 8081:80 --network restaurante-bridge restauranteservice:1.0

7. Run Container ItemService:
   ```bash
   docker run --name itemservice -p 8080:80 --network restaurante-bridge itemservice:1.0

### Observações:
- Certifique-se de substituir quaisquer outras configurações necessárias, como senhas, portas, etc., de acordo com o seu ambiente e requisitos específicos.

- Certifique-se de que a rede Docker restaurante-bridge esteja criada antes de executar os comandos, ou substitua pela rede que você estiver utilizando.

## Contribuição
👨🏽‍💻 Este projeto ainda está em fase de desenvolvimento e pode estar sujeito a mudanças frequentes.
Contribuições são bem-vindas e encorajadas! Sinta-se à vontade para abrir problemas ou enviar solicitações de pull para ajudar a melhorar este projeto :smile:	
