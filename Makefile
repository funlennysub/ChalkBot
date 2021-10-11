.PHONY: run
run:
	docker-compose --profile bot up -d

.PHONY: build
build:
	docker-compose --profile bot build

.PHONY: stop
stop:
	docker network disconnect -f chalkbot_default ChalkBot
	docker network disconnect -f chalkbot_default chalkbot-mongodb
	docker-compose down
