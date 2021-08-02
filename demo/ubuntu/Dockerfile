FROM docker.io/library/ubuntu:18.04@sha256:7bd7a9ca99f868bf69c4b6212f64f2af8e243f97ba13abb3e641e03a7ceb59e8

COPY my-app.sh /usr/bin/my-app

LABEL org.opencontainers.image.base.name=docker.io/library/ubuntu:18.04
LABEL org.opencontainers.image.base.digest=sha256:d1d454df0f579c6be4d8161d227462d69e163a8ff9d20a847533989cf0c94d90

CMD ["/usr/bin/my-app"]