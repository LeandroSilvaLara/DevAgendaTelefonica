# DevAgendaTelefonica


##PROCEDURE Telefone

```CREATE PROCEDURE spTelefone 
(  
    @Id INT = NULL,  
    @Telefone VARCHAR(20) = NULL,
	@ActionType VARCHAR(25)
     
)  
AS  
BEGIN  
    IF @ActionType = 'SaveData'  
    BEGIN  
        IF NOT EXISTS (SELECT * FROM contato WHERE Id=@Id)  
        BEGIN  
            INSERT INTO Telefone (Telefone)  
            VALUES (@Telefone)  
        END  
        ELSE  
        BEGIN  
            UPDATE telefone SET Telefone=@Telefone WHERE Id=@Id  
        END  
    END  
    IF @ActionType = 'DeleteData'  
    BEGIN  
        DELETE telefone WHERE Id=@Id  
    END  
    IF @ActionType = 'FetchData'  
    BEGIN  
        SELECT Id AS EmpId,Telefone FROM telefone  
    END  
    IF @ActionType = 'FetchRecord'  
    BEGIN  
        SELECT Id AS EmpId,Telefone FROM telefone  
        WHERE Id=@Id  
    END  
END 
```
------------------------------------------------------------
##PROCEDURE Contato

```
CREATE PROCEDURE spContato 
(  
    @Id INT = NULL,  
    @Name VARCHAR(20) = NULL,
	@ActionType VARCHAR(25)
     
)  
AS  
BEGIN  
    IF @ActionType = 'SaveData'  
    BEGIN  
        IF NOT EXISTS (SELECT * FROM contato WHERE Id=@Id)  
        BEGIN  
            INSERT INTO contato (Name)  
            VALUES (@Name)  
        END  
        ELSE  
        BEGIN  
            UPDATE contato SET Name=@Name WHERE Id=@Id  
        END  
    END  
    IF @ActionType = 'DeleteData'  
    BEGIN  
        DELETE contato WHERE Id=@Id  
    END  
    IF @ActionType = 'FetchData'  
    BEGIN  
        SELECT Id AS EmpId,Name FROM contato  
    END  
    IF @ActionType = 'FetchRecord'  
    BEGIN  
        SELECT Id AS EmpId,Name FROM contato  
        WHERE Id=@Id  
    END  
END  
```
