# Introdução
Projeto de Fluxo de Caixa simplificado.

# Instruções de Uso

# Visão de negócio

## Requisitos funcionais
* Fazer lançamentos de crédito e débito em um caixa;
* Ter um relatório com o consolidado diário das movimentações no caixa;
* Auditoria sobre as movimentações.

## Requisitos não funcionais
* Escalabilidade;
* Observabilidade;
* Segurança;

## Análise sobre os dados

IMAGEM

Os dados da solução estão representados pelas seguintes entidades:

* Conta;
* Consolidado Diário;
* Lançamento;
* Auditoria;

### Conta
Representa a conta onde os lançamento serão feitos. Seus atributos são:
* ID único;
* Nome;
* Saldo;
* Status (Ativa ou Inativa).

### Consolidado Diário
Representa a consolidação dos lançamentos referentes a um dia. Seus atributos são:
* ID único;
* Data do dia;
* Dia da Semana;
* Dia;
* Mês;
* Ano;
* Total Créditos;
* Total Débitos;
* Saldo;
* Status (Em Aberto, Consolidado);
* Data da Consolidação;
* Conta.

### Lançamento
Representa o lançamento de crédito ou débito feitos na conta. Seus atributos são:
* ID único;
* ID da Transação;
* Tipo (Crédito, Débito);
* Descrição;
* Valor;
* Data do Lançamento;
* Status (Efetivado, Em Análise, Cancelado, Estornado);
* Conta;

### Auditoria
Representa as auditorias realizadas pelo sistema. Seus atributos são:
* ID único;
* Usuário;
* Ação;
* Data da Ação.

## Funcionalidades
### Consulta, cadastro, edição ou inativação de conta
Uma conta pode ser consultada, cadastrada, editada ou inativada. Não é possível excluir uma conta.

### Lançar crédito ou débito
Um lançamento pode ser de crédito ou débito. É preciso informar a conta, a descrição e o valor do lançamento. Todos os lançamentos são feitos no dia corrente.

> **Regra**: Para fazer um lançamento, a conta precisa estar ATIVA;
> **Regra**: Um lançamento de débito não pode ser maior que o saldo em conta;
> **Regra**: Um lançamento só pode ser realizado enquanto o dia está em aberto. Uma vez consolidado não é possíve fazer lançamentos naquele dia.

### Consultar relatório de consolidado diário
É possível consultar os dados do consolidado diário. Os filtros disponíveis são:
* Data do dia;
* Dia da Semana;
* Dia;
* Mês;
* Ano;
* Período de consolidação;
* Status;

### Consultar auditoria
É possível consultar as ações realizadas no sistema. Os filtros disponíveis são o nome do usuário, data da ação e descrição da ação.

# Visão Técnica

## Arquitetura de Referência

IMAGEM

## Componentes

* WEB API;
* Banco de Dados;
* Arquivo de Log.

### WEB API (.NET Core)
Web API que possui os endpoints que expõem as funcionalidades do caixa.

### Banco de Dados (Postgres)
Banco de dados que possui contexto de dados da solução.

### Arquivo de Log
Arquivo de log que registra os logs sistêmicos e de negócio da solução.

## Requisitos não funcionais
### Segurança
Os endpoints da API são auntenticados através de um JWT expirável de 15 minutos de duração. Após esse período, o JWT deve ser renovado.

### Escalabilidade
A API que possui as funcionalidade do caixa é um microserviço stateless que pode ser publicado em containers a fim de ter uma escalabilidade horizontal. Para atingir seu pontencial máximo, é preciso tomar outras medidas arquiteturais como balanceamento de carga e estruturas de cache, por exemplo.

### Observabilidade
A solução possui um log em arquivo que registra todos os erros, exceções e informações do sistema a fim de ter um rastro de todas as atividades da aplicação. Para antigir seu potencial máximo, é preciso reter estes logs em bancos especializados em logs, em aplicações que permitem organizar estes dados em painéis visuais e criar alertas para casos críticos.

## Estrutura da Solução

## Arquitetura em Camadas
IMAGEM

* API
* Data Transfer
* Aplicação
* Domínio
* Infraestrutura
* IOC
* Testes

| Camada | Descrição |
| ------ | ------ |
| API | Camada responsável pelos endpoints da API |
| Data Transfer | Camada responsável pelas classes de transferência de dados entre domínio e aplicação |
| Aplicação | Camada responsável pelos serviços de aplicação que funcionam como orquestradores. |
| Domínio | Camada responsável pelo domínio do problema. |
| Infraestrutura | Camada responsável pelas classes de repositório que dão acesso ao banco, arquivos, logs, filas, etc. |
| IOC | Camada responsável pelas configurações de injeção de dependências e configurações gerais. |
| Testes | Camada responsável por conter os testes unitários de domínio. |

**Exemplo:**