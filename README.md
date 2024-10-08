# Proyecto-mar-Biofilia
Repositorio del proyecto institucional UPB adscrito al proyecto general de regalias liderado por el Parque Explora




### Descripción del Proyecto
Este proyecto utiliza **Unity** junto con **FMOD** para crear una experiencia inmersiva en realidad virtual centrada en el mar Caribe y sus ecosistemas. La experiencia está diseñada para adolescentes y adultos jóvenes, permitiéndoles interactuar con diferentes elementos del entorno, incluidos peces, manglares, y otros componentes del mar Caribe.

### Estructura del Repositorio 

```
/Project-Root
│
├── /UnityProject          # Carpeta principal del proyecto de Unity
│ 
│
├── /FMODSources           # Proyecto completo de FMOD (fuera de Unity)
│   
│
├── /3DModels              # Modelos 3D usados en la experiencia (fuera de Unity)
│            
│
├── /Documentation         # Documentación del proyecto
│   ├── /Actas             # Actas de reuniones y actividades
│   └── /Instructivos      # Guías y manuales de uso y desarrollo
│
└── /VersionedAssets       # Archivos versionados fuera de Unity
```

### Integración de FMOD
El proyecto usa **FMOD** para manejar todo el audio interactivo. Dentro del directorio del proyecto Unity (`/UnityProject/Assets/FMOD`) se encuentran los **bancos de sonido** y **eventos de audio** necesarios para la ejecución del proyecto en Unity.

Los archivos fuente de FMOD, como los archivos `.fspro` y los sonidos sin procesar, se almacenan en la carpeta **FMODSources** fuera del proyecto Unity para evitar sobrecargar el repositorio de Unity con archivos no esenciales.

## Uso de Gitflow

Este proyecto utiliza Gitflow para manejar las ramas y el flujo de trabajo de desarrollo. A continuación, se muestra cómo se estructuran las ramas principales:

- **master**: Contiene el código de producción final.
- **develop**: Ramas de desarrollo que contienen cambios recientes y estables.
- **feature/**: Para el desarrollo de nuevas características.
- **release/**: Para la preparación de versiones de producción.
- **hotfix/**: Para correcciones rápidas de bugs en producción.

### Convención de mensajes de commits

- `feat`: Para la adición de nuevas características.
- `fix`: Para corrección de errores.
- `docs`: Para cambios en la documentación.
- `style`: Cambios que no afectan la lógica del código (formato, espacios en blanco, etc.).
- `assets`: Adición o modificación de archivos 3D, texturas, sonidos, etc.
- `refactor`: Para refactorizaciones de código sin cambios en su comportamiento.
- `new`: Para la adición de nuevos objetos, carpetas o parámetros.

### Ejemplo de  commits
- Tipo de commit: feat
- Mensaje de commit: feat: Interacción de crecimiento del manglar ahora incluye interacción con el agua usando el remo

### Versionado Semántico
El proyecto sigue el esquema de **Versionado Semántico** (Semantic Versioning) para gestionar las versiones, siguiendo el patrón:
- `MAJOR.MINOR.PATCH`
  
1. **MAJOR**: Se incrementa cuando se realizan cambios incompatibles con versiones anteriores.
2. **MINOR**: Se incrementa cuando se agregan funcionalidades de manera retrocompatible.
3. **PATCH**: Se incrementa cuando se realizan correcciones de errores de manera retrocompatible.


### Ejemplo de Versionados

1.Si haces cambios en la interacción de los usuarios con el sistema de audio sin romper las funcionalidades anteriores:  Incrementa el MINOR: 1.1.0.

2.Si encuentras un bug en el sistema de selección de semillas y lo corriges: Incrementa el PATCH: 1.1.1.

3. Si cambias por completo la lógica de interacción de los usuarios y esto afecta el sistema de entrada:
Incrementa el MAJOR: 2.0.0.


### Requisitos de Desarrollo
- **Unity**: Versión 2022.3.36f1
- **FMOD**: Integración con Unity mediante FMOD versión 2.02.
- **Oculus XR Plugin**: Compatible con dispositivos de realidad virtual Oculus Quest.
