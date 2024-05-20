# Guia de configuracion de ML Agents
Este documento sirve para tomar notas de los errores y aciertos en la configuracion de ML Agents.

## Configuracion de entorno
Se instalo Anaconda para la gestion de entornos virtuales de Python.
Los comandos para la instalacion de ML Agents fueron los siguientes:
```bash
conda create -n ml_enemies python=3.9
conda activate ml_enemies
pip install mlagents
```
## Comandos basicos


## Errores
Una vez en el entorno de desarrollo se ejecuta el comando `mlagents-learn --help` para observar las opciones disponibles
o darnos cuenta de un error en caso de existir.

### Error de protobuf
En caso de que se presente un error de protobuf, se debe instalar con el pip una version igual o anterior a la `3.20.x` 
de esta manera se evitan los errores de compatibilidad.