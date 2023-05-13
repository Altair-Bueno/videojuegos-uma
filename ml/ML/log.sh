python3.9 -m venv .venv
.venv/bin/pip install -r ML/requirements.txt
# run-id == "behaviour name"
.venv/bin/mlagents-learn ML/cube_config.yml --run-id=CubeAgent --force

# Ver el progreso del entrenamiento
.venv/bin/tensorboard --logdir results --port 6006
