FROM docker.io/library/ubuntu:18.04@sha256:d1d454df0f579c6be4d8161d227462d69e163a8ff9d20a847533989cf0c94d90

COPY my-app.sh /usr/bin/my-app

LABEL org.opencontainers.image.base.name=docker.io/library/ubuntu:18.04
LABEL org.opencontainers.image.base.digest=sha256:d1d454df0f579c6be4d8161d227462d69e163a8ff9d20a847533989cf0c94d90

CMD ["/usr/bin/my-app"]