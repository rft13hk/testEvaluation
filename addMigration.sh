#!/bin/bash

# Verifica se um argumento foi fornecido
if [ -z "$1" ]; then
  echo "É necessário informar o nome da Migration"
  exit 1
fi

migration_name="$1"
project_path="src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.ORM.csproj"
startup_project_path="src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj"

# Executa o comando dotnet ef migrations add com o parâmetro fornecido
dotnet ef migrations add "$migration_name" --project "$project_path" --startup-project "$startup_project_path"

# Verifica o código de saída do comando dotnet
if [ $? -ne 0 ]; then
  echo "Ocorreu um erro ao executar o comando dotnet ef migrations add."
fi

exit 0
