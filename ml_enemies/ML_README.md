# Guia de configuración de ML Agents
Este documento sirve para tomar notas de los errores y aciertos en la configuración de ML Agents.

## Configuración de entorno
Se instaló Anaconda para la gestion de entornos virtuales de Python.
Los comandos para la instalación de ML Agents fueron los siguientes:
```bash
conda create -n ml_enemies python=3.9
conda activate ml_enemies
pip install mlagents
```
## Comandos básicos
Para activar el entorno de desarrollo se ejecuta el comando `conda activate ml_enemies` y 
para desactivarlo se ejecuta `conda deactivate`.

Para ejecutar el entrenamiento de los modelos es necesario ter un archivo de configuración `.yaml` que se ejecuta con 
el comando `mlagents-learn trainer_config.yaml --run-id=primer_entrenamiento`.

Es posible reutilizar entrenamientos previos con `--resume` para continuar un entrenamiento previo, 
o utilizar `--force` para forzar la creación de un nuevo entrenamiento.

Para lanzar la interfaz de análisis de los modelos se ejecuta el comando `tensorboard --logdir results` y se accede


## Errores
Una vez en el entorno de desarrollo se ejecuta el comando `mlagents-learn --help` para observar las opciones disponibles
o darnos cuenta de un error en caso de existir.

### Error de protobuf
En caso de que se presente un error de protobuf, se debe instalar con el pip una version igual o anterior a la `3.20.x` 
de esta manera se evitan los errores de compatibilidad.