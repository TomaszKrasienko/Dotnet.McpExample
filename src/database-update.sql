CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251101085206_InitialCreate') THEN
    CREATE TABLE "Contractor" (
        "Id" uuid NOT NULL,
        "Name" text NOT NULL,
        "IsDeleted" boolean NOT NULL,
        "DeletedAt" timestamp with time zone,
        CONSTRAINT "PK_Contractor" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251101085206_InitialCreate') THEN
    CREATE TABLE "Order" (
        "Id" uuid NOT NULL,
        "Number" integer NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "Status" text NOT NULL,
        "ContractorId" uuid NOT NULL,
        CONSTRAINT "PK_Order" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Order_Contractor_ContractorId" FOREIGN KEY ("ContractorId") REFERENCES "Contractor" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251101085206_InitialCreate') THEN
    CREATE TABLE "OrderPosition" (
        "Id" uuid NOT NULL,
        "UnitPrice" numeric(18,2) NOT NULL,
        "Quantity" numeric(18,2) NOT NULL,
        "OrderId" uuid NOT NULL,
        CONSTRAINT "PK_OrderPosition" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_OrderPosition_Order_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Order" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251101085206_InitialCreate') THEN
    CREATE INDEX "IX_Order_ContractorId" ON "Order" ("ContractorId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251101085206_InitialCreate') THEN
    CREATE INDEX "IX_OrderPosition_OrderId" ON "OrderPosition" ("OrderId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251101085206_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20251101085206_InitialCreate', '9.0.10');
    END IF;
END $EF$;
COMMIT;

