"# UsuarioApi" 
"# teste" 
"# UsuarioApi" 

API que armazena de retorna informações de usuario, como Nome, Email, Senha e CPF.

O nome é formatado com todas as letras em maisculo
O email é validado, se for invalida retornará um Bad Request,
A senha é salva com criptografia, ao obter todos os usuário, todas as senhas estarão criptogradas, somente quando é retornado pela busca do id, a senha é descriptografada.
O CPF é validado, se for inválido retornará um Bad Request

