import subprocess
import json

# Padrões
framework_padrao = "net8.0"

# Comandos Bash que retornam JSON
command_lista_pacotes_desatalizados = 'dotnet list package --outdated --format json'
command_lista_pacotes_instalados = 'dotnet list package --format json'

# Função para executar um comando e carregar o JSON da saída
def run_command_and_load_json(command):
    try:
        # Executa o comando e captura a saída
        result = subprocess.run(command, shell=True, check=True, stdout=subprocess.PIPE, text=True)
        # Carrega a saída JSON
        return json.loads(result.stdout)
    except subprocess.CalledProcessError as e:
        print(f"Erro ao executar o comando '{command}': {e}")
        return None
    except json.JSONDecodeError as e:
        print(f"Erro ao decodificar JSON do comando '{command}': {e}")
        return None

# Executa os comandos e carrega os JSONs
lista_pacotes_desatalizados = run_command_and_load_json(command_lista_pacotes_desatalizados)
lista_pacotes_instalados = run_command_and_load_json(command_lista_pacotes_instalados)

def valida_dependencias_projeto():
    lista_projetos = lista_pacotes_instalados["projects"]
    lista_projetos_com_framework_errada = []

    for projeto in lista_projetos:
        path_project = projeto["path"]
        lista_frameworks = projeto["frameworks"]
        for project_framework in lista_frameworks:
            if project_framework["framework"] != framework_padrao:
                lista_projetos_com_framework_errada.append(path_project)
            topLevelPackages = project_framework["topLevelPackages"]
            for topLevelPackage in topLevelPackages:
                nome_pacote = topLevelPackage["id"]
                if

    return {
        "lista_projetos_com_framework_errada": lista_projetos_com_framework_errada
    }

def valida_se_pacote_obrigatorio_esta_instado():
    pass

# Exemplo de manipulação dos dados carregados
print("Lista de Pacotes Desatalizados")
print(lista_pacotes_desatalizados)

print("\n-\n")

print("Lista de Pacotes Instalados:")
print(lista_pacotes_instalados)

print("\n-\n")

validacoes = valida_dependencias_projeto()

if len(validacoes["lista_projetos_com_framework_errada"]) > 0:
    print("Projetos com framework diferente de '"+framework_padrao+"'" + str(validacoes["lista_projetos_com_framework_errada"]))