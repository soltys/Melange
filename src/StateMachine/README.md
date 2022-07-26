# Soltys.StateMachine


antlr -package Soltys.StateMachine -Dlanguage=CSharp -o AntlrGen -visitor StateMachine.g4

java org.antlr.v4.Tool -package Soltys.StateMachine -Dlanguage=CSharp -o AntlrGen -visitor StateMachine.g4


PWD is JAVAOUT FOLDER

antlr ..\StateMachine.g4 -o JAVAOUT
javac StateMachine*.java
grun StateMachine stateMachine ..\example.txt -gui
