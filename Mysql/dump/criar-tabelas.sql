CREATE TABLE boleta (
    id CHAR(36) PRIMARY KEY,          -- UUID como string de 36 caracteres
    data DATETIME(6) NOT NULL,        -- Data e hora com precisão de microssegundos
    valor DECIMAL(10, 2) NOT NULL,    -- Valor decimal com duas casas decimais
    tipoBoleta INT NOT NULL,          -- Inteiro para o tipo de boleta
    tipoCanal INT NOT NULL            -- Inteiro para o tipo de canal
 );
