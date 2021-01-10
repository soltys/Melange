import React, { useState, useEffect } from 'react';
import './Basic.css'

export const InternalOnly: React.FunctionComponent = (props) => {
    return clientConfig.backend.isInternal
        ? (
            <>
            {props.children}
            </>
        )
        : (<></>);
}

export const ExternalOnly: React.FunctionComponent = (props) => {
    return !clientConfig.backend.isInternal
        ? (
            <>
            {props.children}
            </>
        )
        : (<></>);
}

export const RecipeList: React.FunctionComponent = (props) => {
    return (
        <ul className="recipe">
            {props.children}
        </ul>
    );
}

type ViewContainerProps = {
    title?: string
}

export const ViewContainer: React.FunctionComponent<ViewContainerProps> = (props) => {
    useEffect(() => {
        const oldTitle = document.title;
        if (props.title) {
            document.title = `${props.title}  - Paweł Sołtysiak`;
        }

        return () => {
            document.title = oldTitle;
        }
    })
    return (
        <div className="view">
            {props.children}
        </div>
    )
}

export const BoxContainer: React.FunctionComponent = (props) => {
    return (
        <div className="box-container">
            {props.children}
        </div>
    )
}

export const Box: React.FunctionComponent = (props) => {
    return (
        <div className="box">
            {props.children}
        </div>
    )
}

type IconButtonProps = {
    iconName: string,
    onClick: (event: React.MouseEvent<HTMLElement, MouseEvent>) => void,
}

export const IconButton: React.FunctionComponent<IconButtonProps> = (props) => {
    return (
        <div className="icon-button" onClick={(e) => props.onClick(e)}>
            <i className={props.iconName}></i>
        </div>
    )
}

type CountdownProps = {
    endDate: Date
}

export const Countdown: React.FunctionComponent<CountdownProps> = (props) => {
    const [daysLeft, setDay] = useState(0);
    const [hoursLeft, setHours] = useState(0);
    const [minutesLeft, setMinutes] = useState(0);
    const [secondsLeft, setSeconds] = useState(0);

    useEffect(() => {

        const updateTick = () => {
            let diff = props.endDate.getTime() - Date.now();

            const days = Math.floor(diff / (1000 * 60 * 60 * 24));
            diff -= days * (1000 * 60 * 60 * 24);

            const hours = Math.floor(diff / (1000 * 60 * 60));
            diff -= hours * (1000 * 60 * 60);

            const minutes = Math.floor(diff / (1000 * 60));
            diff -= minutes * (1000 * 60);

            const seconds = Math.floor(diff / (1000));
            diff -= seconds * (1000);

            setDay(days);
            setHours(hours);
            setMinutes(minutes);
            setSeconds(seconds);
        }

        const updateTimer = window.setInterval(updateTick, 500);

        return () => {
            window.clearInterval(updateTimer);
        }
    }, [props.endDate]);

    return (
        <div className="countdown">
            <strong>Pozostało </strong>
            <table>
                <thead>
                    <tr>
                        <th>Dni</th>
                        <th>Godzin</th>
                        <th>Minut</th>
                        <th>Sekund</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>{daysLeft}</td>
                        <td>{hoursLeft}</td>
                        <td>{minutesLeft}</td>
                        <td>{secondsLeft}</td>
                    </tr>
                </tbody>
            </table>
            <strong>do premiery</strong>
        </div>
    )
}
