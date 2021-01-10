import React, { useState } from 'react';
import './RatioCalculator.css';

type Ratio = {
    coffee: number,
    water: number,
    isPreferred: boolean
}

type RatioCalculatorProps = {
    unit: string,
    inputDescription: string,
    outputDescription: string
}

const RatioCalculator: React.FunctionComponent<RatioCalculatorProps> = (props) => {

    const [input, setInput] = useState(270);

    const onInputChanged = (e: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = parseInt(e.target.value)
        if (isNaN(newValue)) {
            setInput(0)
        } else {
            setInput(newValue)
        }

    }

    const calculation = (input: number, coffee: number, water: number) => {
        return ((input / water) * coffee).toFixed(1);
    }

    const ratios: Ratio[] = [
        { coffee: 1, water: 15, isPreferred: false },
        { coffee: 1, water: 16, isPreferred: false },
        { coffee: 1, water: 17, isPreferred: true },
        { coffee: 1, water: 18, isPreferred: false },
        { coffee: 1, water: 19, isPreferred: false },
    ]

    return (
        <div>
            <p>Chcę wypić <input className="ratio-calculator__input" type="number" defaultValue={input} onChange={(e) => onInputChanged(e)} />{props.unit} {props.inputDescription} </p>
            <p>Potrzebuję:</p>
            <ul className="ratio-list">
                {ratios.map(ratio => (
                    <li key={ratio.water} className={ratio.isPreferred ? "ratio--is-preferred" : ""}>
                        {calculation(input, ratio.coffee, ratio.water)} {props.unit} {props.outputDescription} dla proporcji {ratio.coffee}:{ratio.water} 
                </li>
                ))}
            </ul>
        </div>
    )
}

export default RatioCalculator;