grammar JsonFishOil;

fishOil
    : access ('.' access)* EOF   
    ;

access : NAME
       | NAME '[' NUMBER ']'
       ;


WS
    : [ \t\u000C\r\n]+ -> skip
    ;

NAME
    : [a-zA-Z_][a-zA-Z_0-9]*
    ;


STRING
: '"' (ESC | SAFECODEPOINT)* '"'
;


fragment ESC
   : '\\' (["\\/bfnrt] | UNICODE)
   ;
fragment UNICODE
   : 'u' HEX HEX HEX HEX
   ;
fragment HEX
   : [0-9a-fA-F]
   ;
fragment SAFECODEPOINT
   : ~ ["\\\u0000-\u001F]
   ;

   
NUMBER
   : '-'? INT ('.' [0-9] +)? EXP?
   ;


fragment INT
   : '0' | [1-9] [0-9]*
   ;

// no leading zeros

fragment EXP
   : [Ee] [+\-]? INT
   ;
