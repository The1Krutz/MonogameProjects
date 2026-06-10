run:
	dotnet run --project Game
run-watch:
	dotnet watch --project Game run
test:
	dotnet test --collect:"XPlat Code Coverage" --logger html && \
  make coverage-report
test-setup:
	(cd Test && dotnet tool restore)
test-watch:
	(cd Test && dotnet watch test)
content:
	(cd Game && dotnet tool run mgcb-editor-linux Content/Content.mgcb)
coverage-report:
	(cd Test && dotnet reportgenerator)
open-coverage:
	firefox Test/TestResults/coverage/index.html
clean:
	rm -rf Game/obj Game/bin
	rm -rf Test/obj Test/bin Test/TestResults
	dotnet clean