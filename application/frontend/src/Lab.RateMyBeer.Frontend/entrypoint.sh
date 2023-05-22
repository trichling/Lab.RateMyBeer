#!/usr/bin/env sh

set -eu

function join_by { local IFS="$1"; shift; echo "$*"; }

envsubst '${BLAZOR_ENVIRONMENT}' < /etc/nginx/nginx.conf.template > /etc/nginx/nginx.conf

vars=$(env | awk -F = '{print "$"$1}')
vars=$(join_by ' ' $vars)
echo "Found variables $vars"

for file in /usr/share/nginx/html/wwwroot/*.json; do
  echo "Processing $file ...";

  cp $file $file.tmpl
  envsubst "$vars" < $file.tmpl > $file
  rm $file.tmpl
done

exec "$@"
