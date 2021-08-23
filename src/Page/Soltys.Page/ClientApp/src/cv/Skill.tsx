import React from 'react';

import './Skill.css'
export const Skill: React.FunctionComponent<{ name: string }> = (props) => {
    return (
        <li className="skill">{props.name}</li>
    )
}

export const SkillList: React.FunctionComponent = (props) => {
    return (
        <div className="skill-container">
            <ul className="skill-list">
                {props.children}
            </ul>
        </div>
    )
}
