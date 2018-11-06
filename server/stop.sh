ENV=dev
ENV_CFG_PATH=config/env/${ENV}/env.ini
source ${ENV_CFG_PATH}

echo -e "stop $SERVER:\c"
echo "$(${PROD_DIR}/$SERVER/bin/$SERVER stop)"