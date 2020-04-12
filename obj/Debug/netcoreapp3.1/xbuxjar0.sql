CREATE TABLE [ShoppingCartItems] (
    [ShoppingCartItemId] int NOT NULL IDENTITY,
    [ItemId] int NULL,
    [Amount] int NOT NULL,
    [ShoppingCartId] nvarchar(max) NULL,
    CONSTRAINT [PK_ShoppingCartItems] PRIMARY KEY ([ShoppingCartItemId]),
    CONSTRAINT [FK_ShoppingCartItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([ItemId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ShoppingCartItems_ItemId] ON [ShoppingCartItems] ([ItemId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200407201728_Add ShoppingCartItem SMigration', N'3.1.3');

GO

