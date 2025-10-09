create table endereco (
id_end integer not null primary key auto_increment,
nome_cidade varchar(30),
rua_end varchar (300),
numero_end varchar(4),
bairro_end varchar (100)
);

insert into endereco values(null,'Ji-Paran�', 'Manoel Franco', '2627', 'Nova Bras�lia');
insert into endereco values(null,'Ji-Paran�', 'Avenida Brasil', '3637', 'Nova Bras�lia');
insert into endereco values(null,'Ji-Paran�', 'Maring�', '1528', 'Nova Bras�lia');
insert into endereco values(null,'Ji-Paran�', 'Manoel Franco', '2725', 'Nova Bras�lia' );
insert into endereco values(null,'Ji-Paran�', 'Projetada', '2222', 'Nova Bras�lia');

create table cliente(
id_cli int not null primary key auto_increment,
nome_cli varchar(100),
telefone_cli varchar(14),
cpf_cli varchar(14),
data_nascimento_cli date,
rg_cli varchar(7),
id_end_fk int ,
foreign key (id_end_fk) references endereco (id_end)
);

insert into cliente values(null,'Luis', '(69)00000-0000', '000.000.000-00', '2008-04-05', '1503050', 1);
insert into cliente values(null,'Carlos', '(69)11111-0000', '000.111.000-00', '2009-10-05', '1201150', 2);
insert into cliente values(null,'Arthur', '(69)00000-1111', '000.000.111-00', '2007-02-25', '1523456', 3);
insert into cliente values(null,'Lucas', '(69)10101-0101', '000.000.000-11', '1980-04-18', '5567051', 4);
insert into cliente values(null,'Felipe', '(69)01010-1010', '001.001.001-01', '2006-08-15', '1512345', 5);

create table funcionario(
id_fun int not null primary key auto_increment,
nome_fun varchar(100),
telefone_fun varchar(14),
cpf_fun varchar(14),
data_nascimento_fun date,
ctps_fun varchar(12),
rg_fun varchar(7),
email_fun varchar(100),
id_end_fk int not null,
foreign key (id_end_fk) references endereco (id_end)
);

create table servico (
  id_serv int not null primary key auto_increment,
  nome_serv varchar(100),
  preco_serv decimal(10,2),
  duracao_min int
);


create table estoque (
  id_est int not null primary key auto_increment,
  descricao_est varchar(200),
  quantidade_est int
);


create table produto (
  id_prod int not null primary key auto_increment,
  nome_prod varchar(100),
  preco_prod decimal(10,2),
  id_est_fk int not null,
  foreign key (id_est_fk) references estoque(id_est)
);

create table fornecedor (
  id_forn int not null primary key auto_increment,
  nome_forn varchar(100),
  telefone_forn varchar(14),
  cnpj_forn varchar(18),
  email_forn varchar(100)
);


create table compra (
  id_comp int not null primary key auto_increment,
  data_comp date,
  valor_total decimal(10,2),
  id_forn_fk int not null,
  foreign key (id_forn_fk) references fornecedor(id_forn)
);

-- Relacionar produtos comprados
create table compra_produto (
  id_comp int not null,
  id_prod int not null,
  quantidade int,
  primary key (id_comp, id_prod),
  foreign key (id_comp) references compra(id_comp),
  foreign key (id_prod) references produto(id_prod)
);

-- Vendas (para clientes)
create table venda (
  id_venda int not null primary key auto_increment,
  data_venda date,
  valor_total decimal(10,2),
  id_cli_fk int not null,
  id_fun_fk int not null,
  foreign key (id_cli_fk) references cliente(id_cli),
  foreign key (id_fun_fk) references funcionario(id_fun)
);

-- Relacionar produtos vendidos
create table venda_produto (
  id_venda int not null,
  id_prod int not null,
  quantidade int,
  primary key (id_venda, id_prod),
  foreign key (id_venda) references venda(id_venda),
  foreign key (id_prod) references produto(id_prod)
);

-- Relacionar servi�os realizados
create table venda_servico (
  id_venda int not null,
  id_serv int not null,
  primary key (id_venda, id_serv),
  foreign key (id_venda) references venda(id_venda),
  foreign key (id_serv) references servico(id_serv)
);

-- Caixa (movimento financeiro)
create table caixa (
  id_caixa int not null primary key auto_increment,
  data_caixa date,
  saldo decimal(10,2)
);

-- Notas fiscais (associadas �s vendas)
create table nota_fiscal (
  id_nf int not null primary key auto_increment,
  numero_nf varchar(20),
  data_nf date,
  valor_nf decimal(10,2),
  id_venda_fk int not null,
  foreign key (id_venda_fk) references venda(id_venda)
);

-- Agendamento (hor�rios de clientes)
create table agendamento (
  id_ag int not null primary key auto_increment,
  data_ag datetime,
  id_cli_fk int not null,
  id_fun_fk int not null,
  id_serv_fk int not null,
  foreign key (id_cli_fk) references cliente(id_cli),
  foreign key (id_fun_fk) references funcionario(id_fun),
  foreign key (id_serv_fk) references servico(id_serv)
);

-- Planos mensais (assinaturas de clientes)
create table plano_mensal (
  id_plano int not null primary key auto_increment,
  nome_plano varchar(100),
  preco_plano decimal(10,2),
  duracao_meses int
);

-- Assinatura de planos pelos clientes
create table cliente_plano (
  id_cli int not null,
  id_plano int not null,
  data_inicio date,
  data_fim date,
  primary key (id_cli, id_plano),
  foreign key (id_cli) references cliente(id_cli),
  foreign key (id_plano) references plano_mensal(id_plano)
);

-- Despesas (contas a pagar)
create table despesa (
  id_desp int not null primary key auto_increment,
  descricao varchar(200),
  valor decimal(10,2),
  data_desp date
);

-- Recebimentos (pagamentos feitos pelos clientes)
create table recebimento (
  id_rec int not null primary key auto_increment,
  id_venda_fk int not null,
  data_rec date,
  valor_rec decimal(10,2),
  metodo_pagamento varchar(50),
  foreign key (id_venda_fk) references venda(id_venda)
);