grammar StateMachine;

stateMachine
    : attribute* state_def+ EOF
    ;

attribute
    : NAME NAME EOS
    ;
state_def
    : STATE_ NAME L_BRACES state_entry* R_BRACES
    | PROCESS_ NAME L_BRACES state_entry* state_def* R_BRACES
    ;
state_entry
    : transition event? EOS
    | visitEvent EOS
    ;

transition
    : AUTO_TRANSITION BANG? NAME
    | MANUAL_TRANSITION BANG? NAME
    ;

event
    : '@' NAME
    ;

visitEvent
    : 'onEnter' action_list? transition?
    | 'onExit' action_list? transition?
    ;


NAME: [a-zA-Z_][a-zA-Z_0-9]*
    ;


action_list
   : L_BRACKET NAME (',' NAME)* R_BRACKET
   ;

STATE_: 'state';
PROCESS_: 'process';

AUTO_TRANSITION: '->';
MANUAL_TRANSITION: '=>';

BANG: '!';

L_BRACKET: '[';
R_BRACKET: ']';

L_PAREN: '(' ;
R_PAREN: ')' ;

L_BRACES: '{';
R_BRACES: '}';

EOS: ([\r\n]+); 
WS: [ \t]+  -> channel(HIDDEN);
