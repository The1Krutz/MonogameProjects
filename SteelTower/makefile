run:
	dotnet run --project SteelTower
run-watch:
	dotnet watch --project SteelTower run
test:
	dotnet test --collect:"XPlat Code Coverage" --logger html && \
  make coverage-report
test-setup:
	(cd SteelTower.Test && dotnet tool restore)
test-watch:
	dotnet watch --project SteelTower.Test test
content:
	(cd SteelTower && dotnet tool run mgcb-editor-linux Content/Content.mgcb)
coverage-report:
	(cd SteelTower.Test && dotnet reportgenerator)
open-coverage:
	firefox SteelTower.Test/TestResults/coverage/index.html
clean:
	rm -rf SteelTower/obj SteelTower/bin
	rm -rf SteelTower.Test/obj SteelTower.Test/bin SteelTower.Test/TestResults
	dotnet clean