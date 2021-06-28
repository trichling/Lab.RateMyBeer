#!/usr/bin/env sh

set -eu

envsubst '${BLAZOR_ENVIRONMENT}' < /etc/nginx/nginx.conf.template > /etc/nginx/nginx.conf

exec "$@"