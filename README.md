MxPdv - Mini Sistema de Ponto de Venda
O MxPdv é uma aplicação desktop simplificada para gestão de vendas (PDV), desenvolvida em C# com Windows Forms. O sistema permite o controlo de utilizadores, produtos, grupos de produtos e a realização de vendas com atualização automática de stock e dashboard de resultados.

🏗️ Arquitetura do Projeto
O projeto segue uma estrutura organizada em camadas para facilitar a manutenção e a separação de responsabilidades:

Entities (Entidades): Define os modelos de dados do domínio, como Produto, Venda, Usuario e GrupoProduto.

Data (Dados/Persistência): Utiliza o Entity Framework 6 com a abordagem Code First. Inclui mapeamentos (Fluent API) para configurar as tabelas do banco de dados de forma detalhada.

Interfaces: Define os contratos (abstrações) para os serviços, promovendo o desacoplamento.

Services (Serviços): Camada de lógica de negócio onde residem as regras para salvar, excluir e validar dados, além de gerir transações de venda.

Views (Interface): Formulários Windows Forms para interação com o utilizador.

Helpers: Classes utilitárias, como a SecurityHelper para encriptação de senhas usando o algoritmo SHA256.

🚀 Funcionalidades Principais
Autenticação Segura: Sistema de login com proteção de senha via hash SHA256.

Gestão de Cadastros: CRUD completo para Grupos de Produtos, Produtos e Utilizadores.

Ponto de Venda (PDV): Interface otimizada para lançamento de itens, com validação de stock em tempo real e fecho de venda com transação na base de dados.

Dashboard: Painel principal que exibe o total de vendas do dia, quantidade de vendas realizadas e alertas de produtos com baixo stock.

Atalhos de Teclado: Focado na produtividade do operador (ex: F2 para novo, F3 para salvar/finalizar).

🛠️ Tecnologias Utilizadas
Linguagem: C#.

Plataforma: .NET Framework 4.6.2.

Interface: Windows Forms.

ORM: Entity Framework 6.5.1.

Base de Dados: SQL Server (Instância sugerida: .\SQLMAXIMUS).

⚙️ Configuração e Instalação
Base de Dados: O sistema está configurado para ligar-se a uma instância SQL Server local denominada SQLMAXIMUS. Pode alterar a connectionString no arquivo App.config:

XML
<add name="MxPdvContext" 
     connectionString="Data Source=.\SQLMAXIMUS;Initial Catalog=MxPdv_DB;Integrated Security=True;" 
     providerName="System.Data.SqlClient" />
Dependências: Certifique-se de restaurar os pacotes NuGet (Entity Framework) ao abrir a solução.

Utilizador Padrão: Na primeira execução, o sistema cria automaticamente um utilizador administrador:

Login: admin

Senha: admin

⌨️ Atalhos do Sistema
[F2]: Iniciar novo registo.

[F3]: Salvar registo ou Finalizar Venda.

[F4]: Excluir registo selecionado.

[F5]: Cancelar Venda atual.

[F8]: Consultar/Selecionar Produto no PDV.

[Enter]: Navegar entre campos ou confirmar ações.
