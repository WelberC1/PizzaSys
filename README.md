# 🍕 PizzaSys API (Proposta)

## 📌 Visão Geral

O **PizzaSys** é uma API REST que será desenvolvida em **.NET 10**, com o objetivo de fornecer uma solução completa para o gerenciamento de uma pizzaria.

A proposta do projeto é criar uma base sólida, organizada e escalável, capaz de atender operações reais como:

* Gestão de pedidos
* Controle de produção (cozinha)
* Gerenciamento de cardápio
* Monitoramento de custos e lucro
* Geração de relatórios estratégicos

---

## 🎯 Objetivo

Desenvolver um backend robusto que permita:

* Clientes realizarem pedidos de forma simples
* A cozinha gerenciar a produção em tempo real
* Gestores acompanharem métricas e resultados do negócio

O sistema será projetado desde o início com foco em **manutenibilidade, clareza e evolução futura**.

---

## 🧠 Arquitetura Proposta

O projeto seguirá os princípios da **Clean Architecture**, garantindo separação de responsabilidades e baixo acoplamento.

```text
src/
 ├── PizzaSys.Domain
 ├── PizzaSys.Application
 ├── PizzaSys.Infra.Data
 └── PizzaSys.API
```

### 🔹 Camadas

* **Domain**

  * Entidades
  * Enums
  * Regras de negócio

* **Application**

  * Serviços de aplicação
  * Orquestração de operações

* **Infra.Data**

  * Persistência de dados
  * Entity Framework Core
  * Mapeamentos

* **API**

  * Controllers REST
  * Exposição dos endpoints

---

## 📦 Modelagem de Domínio (DDD)

O sistema será modelado com base em conceitos de **Domain-Driven Design (DDD)**, buscando representar fielmente o negócio.

Principais entidades previstas:

* Usuário
* Endereço
* Categoria de Produto
* Produto
* Pedido
* Item de Pedido

---

## 🧩 Princípios de Engenharia

O desenvolvimento seguirá boas práticas como:

* **SOLID**
* Baixo acoplamento
* Clean Code
* **Autenticação e Autorização (JWT e Roles na Entidade Usuário)**
O objetivo principal é otimizar a realização de testes e facilitar a inclusão de novas funcionalidades.

---

## 👥 Perfis de Usuário

O sistema contará com três tipos principais de usuários:

### 🧑 Cliente

* Visualizar cardápio
* Realizar pedidos
* Acompanhar status

### 👨‍🍳 Pizzaiolo

* Visualizar pedidos em aberto
* Atualizar status dos pedidos
* Cadastrar e gerenciar produtos

### 🧑‍💼 Gerente

* Gerenciar usuários
* Gerenciar produtos e categorias
* Acompanhar relatórios e métricas

A aplicação não permitirá que os usuários façam ações que não são permitidas em seu tipo de usuário. Por exemplo, um cliente não poderá cadastrar ou gerenciar um produto.

---

## 🍕 Funcionalidades Planejadas

### 📋 Gestão de Cardápio

* Cadastro de produtos
* Organização por categorias (Pizza, Bebida, etc.)
* Controle de ativação/desativação
* Definição de preço de venda e custo de produção

---

### 🛒 Gestão de Pedidos

* Criação de pedidos por clientes
* Adição de múltiplos itens
* Controle de status do pedido:

```text
Criado → EmPreparacao → Pronto → SaiuParaEntrega → Entregue
```

* Cancelamento controlado

---

### 💰 Controle de Custos e Lucro

O sistema permitirá:

* Definição de custo de produção por produto (material + mão de obra)
* Cálculo automático de:

  * Valor total do pedido
  * Custo total
  * Lucro

Também será implementado o conceito de **snapshot de custo**, garantindo consistência histórica dos dados.

---

### 📊 Relatórios Gerenciais

Serão disponibilizados indicadores como:

* Faturamento por período
* Custo total
* Lucro
* Ticket médio
* Produtos mais vendidos
* Categorias mais vendidas

---

## 🗄️ Persistência de Dados

Os dados não serão armazenados em memória.

O sistema utilizará:

* **SQL Server** como banco de dados relacional
* **Entity Framework Core** como ORM
* Mapeamentos via Fluent API

Também serão aplicadas boas práticas como:

* Índices (ex: e-mail único)
* Controle de precisão decimal
* Relacionamentos bem definidos

---

## 🚀 Tecnologias

* .NET 10
* C#
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server

---

## 📈 Evolução Planejada

O projeto será desenvolvido com possibilidade de expansão futura, incluindo:

* Integração com sistemas de pagamento
* Controle de estoque
* Notificações

---

## 💬 Considerações Finais

Este projeto tem como objetivo aperfeiçoar minhas habilidades e ajudar a solucionar um problema real e desafiador, que é a gestão de restaurantes e pizzarias. 
