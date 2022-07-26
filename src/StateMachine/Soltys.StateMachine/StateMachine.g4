grammar StateMachine;

stateMachine
    : state_def+ EOF   
    ;

attribute
    : IDEN IDEN EOS
    ;
state_def
    : STATE_ IDEN  L_BRACES EOS state_entry* R_BRACES EOS #StateDefinition
    | PROCESS_ IDEN L_BRACES EOS state_entry* state_def* R_BRACES EOS #ProcessDefinition
    | META L_BRACES EOS attribute+ R_BRACES EOS   #MetaDefinition
    ;
state_entry
    : transition trigger? EOS
    | event EOS
    ;

transition
    : AUTO_TRANSITION BANG? IDEN
    | MANUAL_TRANSITION BANG? IDEN
    ;

trigger
    : '@' IDEN
    ;

event
    : 'onEnter' action_list? transition?
    | 'onExit' action_list? transition?
    ;



    
action_list
   : L_BRACKET IDEN (',' IDEN)* R_BRACKET
   ;

STATE_: 'state';
PROCESS_: 'process';
META: 'meta';

AUTO_TRANSITION: '->';
MANUAL_TRANSITION: '=>';

BANG: '!';

L_BRACKET: '[';
R_BRACKET: ']';

L_PAREN: '(' ;
R_PAREN: ')' ;

L_BRACES: '{';
R_BRACES: '}';


IDEN: [a-zA-Z_][a-zA-Z_0-9]*;

EOS: ([\r\n]+);

WS: [ \t\u000C]+ -> skip;




LINE_COMMENT : '//' ~[\r\n]*      -> channel(HIDDEN);

INVALID_CHAR: .;
