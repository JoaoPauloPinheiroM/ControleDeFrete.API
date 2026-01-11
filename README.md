# Controle de Frete - Sistema de Gestão Logística

Este projeto é um sistema completo para gestão de fretes, motoristas, veículos e clientes. Desenvolvido utilizando as tecnologias mais recentes do ecossistema .NET, incluindo uma API robusta e um frontend interativo em Blazor WebAssembly.

## 🚀 Tecnologias Utilizadas

*   **.NET 10.0**: Plataforma base para o desenvolvimento do backend e frontend.
*   **ASP.NET Core Web API**: Backend para gerenciamento de dados e regras de negócio.
*   **Blazor WebAssembly**: Frontend SPA (Single Page Application) rodando no navegador do cliente.
*   **Entity Framework Core 10.0**: ORM para mapeamento objeto-relacional com SQL Server.
*   **SQL Server**: Banco de dados relacional.
*   **NSwag**: Documentação interativa da API (Swagger/OpenAPI).
*   **Scrutor**: Biblioteca para injeção de dependência por convenção (scanning).
*   **Arquitetura Limpa (Clean Architecture)**: Organização do projeto visando manutenibilidade e testabilidade.

## 📂 Estrutura do Projeto

O projeto está dividido em camadas lógicas para melhor separação de responsabilidades:

*   **`ControleDeFrete.API`**: Projeto principal da API, contém os Controllers e configuração de injeção de dependência.
*   **`ControleDeFrete.Application`**: Camada de aplicação contendo Casos de Uso (Services), DTOs e Interfaces. Implementa a lógica de orquestração.
*   **`ControleDeFrete.Domain`**: Núcleo do projeto contendo Entidades (`Frete`, `Motorista`, etc.), Enums e Interfaces de Repositório. Aqui residem as **Regras de Negócio**.
*   **`ControleDeFrete.Infrastructure`**: Implementação de acesso a dados (EF Core), migrações e configurações de banco.
*   **`ControleDeFrete.WebAssembly`**: Aplicação front-end desenvolvida com Blazor.

## ⚙️ Configuração e Execução

### Pré-requisitos
*   [.NET SDK 10.0](https://dotnet.microsoft.com/download) (versão preview ou release candidate compatível)
*   SQL Server (LocalDB ou instância dedicada)
*   Visual Studio 2022 ou VS Code

### Passo a Passo

1.  **Configurar Banco de Dados**:
    *   Verifique a string de conexão no arquivo `ControleDeFrete.API/appsettings.Development.json`. O padrão é `DefaultConnection` apontando para o LocalDB (`FreteAPIV0`).
    *   Aplique as migrações para criar o banco de dados:
        ```bash
        cd ControleDeFrete.Infrastructure
        dotnet ef database update --startup-project ../ControleDeFrete.API
        ```

2.  **Executar a API**:
    *   No diretório `ControleDeFrete.API`:
        ```bash
        dotnet run
        ```
    *   A API estará disponível (por padrão) em `https://localhost:7246` (verifique o `launchSettings.json` para confirmar a porta).
    *   Você pode acessar a documentação Swagger em `/swagger`.

3.  **Executar o Frontend (Blazor)**:
    *   No diretório `ControleDeFrete.WebAssembly`:
        ```bash
        dotnet run
        ```
    *   O frontend estará disponível em `https://localhost:7142` (verifique o `launchSettings.json`).

## 📏 Regras de Negócio e Fluxos

### Gestão de Fretes
O ciclo de vida de um Frete é controlado pelo `Status` e possui rigorosas validações:

1.  **Criação (Pendente)**:
    *   Um frete é criado no estado `Pendente`.
    *   Neste estado, é possível alterar as alocações de **Motorista** e **Veículo**.
2.  **Alocação**:
    *   `AtribuirMotorista` e `AtribuirVeiculo`: Só permitidos enquanto o frete é `Pendente`.
3.  **Início de Trânsito (Em Trânsito)**:
    *   Para iniciar, o frete deve estar `Pendente`.
    *   **Obrigatório**: Ter Motorista e Veículo atribuídos.
    *   Ao iniciar, o status muda para `EmTransito` e a `DataCarregamento` é registrada.
4.  **Entregas e Finalização**:
    *   Fretes podem ter múltiplas entregas sequenciais.
    *   `FinalizarEntrega`: Marca uma entrega específica como realizada.
    *   Quando **todas** as entregas do frete são concluídas, o status do frete muda automaticamente para `Finalizado` e a `DataEntrega` é registrada.

### Motoristas
*   **Cadastro**: Requer Nome, CPF (validado), CNH e Endereço.
*   **Inativação**: Não é possível inativar um motorista que possua um frete "Em Curso" (associado a um frete não finalizado).

### Veículos
*   **Cadastro**: Requer Placa (validada), Modelo, Marca e Ano de Fabricação.
*   **Validação**: Ano de fabricação deve ser entre 1900 e o ano atual.

## 🛠️ Desenvolvimento e Extensão

Para adicionar novas funcionalidades:
1.  Defina as novas Entidades ou altere as existentes em `ControleDeFrete.Domain`.
2.  Crie as Interfaces de Repositório em `ControleDeFrete.Domain/Interfaces`.
3.  Implemente os Repositórios e configurações do EF em `ControleDeFrete.Infrastructure`.
4.  Crie os DTOs e Serviços de Aplicação em `ControleDeFrete.Application`. Lembre-se de usar a convenção de nomenclatura (ex: `Criar...`, `Consultar...`) para que o **Scrutor** registre automaticamente os serviços.
5.  Exponha a funcionalidade via Controllers na `ControleDeFrete.API`.
6.  Consuma a API no `ControleDeFrete.WebAssembly`.
