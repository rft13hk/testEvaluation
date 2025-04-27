#!/bin/bash

# Verifica se um argumento foi fornecido
if [ -z "$1" ]; then
  echo "É necessário informar o nome da Migration a ser removida (ou 'Last' para remover a última)"
  exit 1
fi

migration_name="$1"
project_path="src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.ORM.csproj"
startup_project_path="src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj"

# Executa o comando dotnet ef migrations remove com o parâmetro fornecido
if [ "$migration_name" == "Last" ]; then
  dotnet ef migrations remove --project "$project_path" --startup-project "$startup_project_path"
else
  dotnet ef migrations remove "$migration_name" --project "$project_path" --startup-project "$startup_project_path"
fi

# Verifica o código de saída do comando dotnet
if [ $? -ne 0 ]; then
  echo "Ocorreu um erro ao executar o comando dotnet ef migrations remove."
fi

exit 0
