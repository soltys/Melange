import React from 'react';
import './Section.css';

type SectionProps = {
    header: string
}
const Section: React.FunctionComponent<SectionProps> = props => {
    return (
        <div>
            <h2>{props.header}</h2>
            {props.children}
        </div>
    )
}
export default Section;