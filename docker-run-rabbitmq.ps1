docker run -d --rm --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=guest rabbitmq:3-management

# http://localhost:15672
# Username: guest
# Passwort: guest

# docker stop some-rabbit

