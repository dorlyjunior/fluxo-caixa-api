/*
 Navicat Premium Data Transfer

 Source Server         : bancos-de-desenvolvimento
 Source Server Type    : PostgreSQL
 Source Server Version : 130003
 Source Host           : localhost:5432
 Source Catalog        : postgres
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 130003
 File Encoding         : 65001

 Date: 27/07/2023 17:30:47
*/


-- ----------------------------
-- Sequence structure for seq_auditoria
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."seq_auditoria";
CREATE SEQUENCE "public"."seq_auditoria" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for seq_consolidado_diario
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."seq_consolidado_diario";
CREATE SEQUENCE "public"."seq_consolidado_diario" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for seq_conta
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."seq_conta";
CREATE SEQUENCE "public"."seq_conta" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for seq_lancamento
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."seq_lancamento";
CREATE SEQUENCE "public"."seq_lancamento" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Table structure for auditoria
-- ----------------------------
DROP TABLE IF EXISTS "public"."auditoria";
CREATE TABLE "public"."auditoria" (
  "id" int4 NOT NULL,
  "usuario" varchar(200) COLLATE "pg_catalog"."default",
  "acao" varchar(350) COLLATE "pg_catalog"."default",
  "data_acao" timestamp(6)
)
;

-- ----------------------------
-- Table structure for consolidado_diario
-- ----------------------------
DROP TABLE IF EXISTS "public"."consolidado_diario";
CREATE TABLE "public"."consolidado_diario" (
  "id" int4 NOT NULL,
  "id_conta" int4 NOT NULL,
  "data_dia" date NOT NULL,
  "dia_semana" varchar(15) COLLATE "pg_catalog"."default" NOT NULL,
  "dia" int2 NOT NULL,
  "mes" int2 NOT NULL,
  "ano" int2 NOT NULL,
  "total_credito" numeric(10,2),
  "total_debito" numeric(10,2),
  "saldo" numeric(10,2),
  "data_consolidacao" timestamp(6),
  "status" int2 NOT NULL
)
;
COMMENT ON COLUMN "public"."consolidado_diario"."id" IS 'ID único do registro';
COMMENT ON COLUMN "public"."consolidado_diario"."id_conta" IS 'ID da conta do saldo diário';
COMMENT ON COLUMN "public"."consolidado_diario"."data_dia" IS 'Data do dia';
COMMENT ON COLUMN "public"."consolidado_diario"."dia_semana" IS 'Dia da semana';
COMMENT ON COLUMN "public"."consolidado_diario"."dia" IS 'Dia do mês';
COMMENT ON COLUMN "public"."consolidado_diario"."mes" IS 'Mês do ano';
COMMENT ON COLUMN "public"."consolidado_diario"."ano" IS 'Ano';
COMMENT ON COLUMN "public"."consolidado_diario"."total_credito" IS 'Total de créditos';
COMMENT ON COLUMN "public"."consolidado_diario"."total_debito" IS 'Total de débitos';
COMMENT ON COLUMN "public"."consolidado_diario"."saldo" IS 'Saldo total do dia';
COMMENT ON COLUMN "public"."consolidado_diario"."data_consolidacao" IS 'Data da consolidação do dia';
COMMENT ON COLUMN "public"."consolidado_diario"."status" IS 'Status da consolidação: 1 = Consolidado; 2 = Em Aberto;';

-- ----------------------------
-- Table structure for conta
-- ----------------------------
DROP TABLE IF EXISTS "public"."conta";
CREATE TABLE "public"."conta" (
  "id" int4 NOT NULL,
  "nome" varchar(200) COLLATE "pg_catalog"."default",
  "saldo" numeric(10,2),
  "status" int2
)
;
COMMENT ON COLUMN "public"."conta"."id" IS 'Id único do registro';
COMMENT ON COLUMN "public"."conta"."nome" IS 'Nome da conta';
COMMENT ON COLUMN "public"."conta"."saldo" IS 'Saldo atual';
COMMENT ON COLUMN "public"."conta"."status" IS 'Status da conta: 1 -  Ativa; 2 - Inativa';

-- ----------------------------
-- Table structure for lancamento
-- ----------------------------
DROP TABLE IF EXISTS "public"."lancamento";
CREATE TABLE "public"."lancamento" (
  "id" int4 NOT NULL,
  "id_conta" int4,
  "id_transacao" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  "tipo" int2 NOT NULL,
  "descricao" varchar(200) COLLATE "pg_catalog"."default",
  "valor" numeric(10,2),
  "data_lancamento" timestamp(6),
  "status" int2
)
;
COMMENT ON COLUMN "public"."lancamento"."id" IS 'ID único do registro. É gerado por uma sequence.';
COMMENT ON COLUMN "public"."lancamento"."id_conta" IS 'Id da conta onde o lançamento é feito.';
COMMENT ON COLUMN "public"."lancamento"."id_transacao" IS 'UUID que representa a transação.';
COMMENT ON COLUMN "public"."lancamento"."tipo" IS 'Tipo do lançamento: 1 - Crédito; 2 - Débito;';
COMMENT ON COLUMN "public"."lancamento"."descricao" IS 'Descrição sobre o lançamento.';
COMMENT ON COLUMN "public"."lancamento"."valor" IS 'Valor financeiro do lançamento';
COMMENT ON COLUMN "public"."lancamento"."data_lancamento" IS 'Data do lançamento';
COMMENT ON COLUMN "public"."lancamento"."status" IS 'Status do lançamento: 1 - Efetivado; 2 - Em Análise; 3 - Cancelado; 4 - Estornado;';

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
SELECT setval('"public"."seq_auditoria"', 8, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
SELECT setval('"public"."seq_consolidado_diario"', 6, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
SELECT setval('"public"."seq_conta"', 1, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
SELECT setval('"public"."seq_lancamento"', 31, true);

-- ----------------------------
-- Primary Key structure for table auditoria
-- ----------------------------
ALTER TABLE "public"."auditoria" ADD CONSTRAINT "auditoria_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table consolidado_diario
-- ----------------------------
ALTER TABLE "public"."consolidado_diario" ADD CONSTRAINT "consolidado_diario_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table conta
-- ----------------------------
ALTER TABLE "public"."conta" ADD CONSTRAINT "conta_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table lancamento
-- ----------------------------
ALTER TABLE "public"."lancamento" ADD CONSTRAINT "lancamento_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Foreign Keys structure for table consolidado_diario
-- ----------------------------
ALTER TABLE "public"."consolidado_diario" ADD CONSTRAINT "fk_consolidado_diario_conta_1" FOREIGN KEY ("id_conta") REFERENCES "public"."conta" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table lancamento
-- ----------------------------
ALTER TABLE "public"."lancamento" ADD CONSTRAINT "fk_lancamento_conta_1" FOREIGN KEY ("id_conta") REFERENCES "public"."conta" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
