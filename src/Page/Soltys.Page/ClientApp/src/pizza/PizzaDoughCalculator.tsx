import React, { useState } from 'react';
import './PizzaDoughCalculator.css';

type PizzaSettingInputProps = {
    name: string,
    unit?: string,
    onValueChanged: (val: number) => void,
    defaultValue?: number,
    step?: number,
    suggested?: string

}

const PizzaSettingInput: React.FunctionComponent<PizzaSettingInputProps> = (props) => {
    const onValueChanged = (e: React.ChangeEvent<HTMLInputElement>) => {
        const val = e.target.valueAsNumber;
        if (isNaN(val) || val < 0) {
            props.onValueChanged(0)
        } else {
            props.onValueChanged(val)
        }
    }
    return (
        <tr>
            <td>{props.name}</td>
            <td><input type="number" className="pizza-dough-calculator__input"
                defaultValue={props.defaultValue} onChange={(e) => onValueChanged(e)} min="0" step={props.step} /></td>
            <td>{props.unit}</td>
            <td className="pizza-dough-suggested">{props.suggested}</td>
        </tr>
    )
}

type PizzaRecipeOutputProps = {
    name: string,
    value: number,
    unit?: string
}
const PizzaRecipeOutput: React.FunctionComponent<PizzaRecipeOutputProps> = (props) => {
    return (
        <tr>
            <td>{props.name}</td>
            <td className="pizza-dough-calculator__output">{props.value.toFixed(0)}</td>
            <td>{props.unit}</td>
        </tr>
    )
}


enum Shape {
    Round = "Round",
    Rectangular = "Rectangular"
}

type PizzaSettingShapeSelectProps = {
    name: string,
    onValueChanged: (val: Shape) => void,

}
const PizzaSettingShapeSelect: React.FunctionComponent<PizzaSettingShapeSelectProps> = (props) => {
    const onValueChanged = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const val = e.target.value;
        switch (val) {
            case Shape.Round:
            case Shape.Rectangular:
                props.onValueChanged(val)
        }
    }
    return (
        <tr>
            <td>{props.name}</td>
            <td>
                <select className="pizza-dough-calculator__input" defaultValue={Shape.Round} onChange={(v) => onValueChanged(v)}>
                    <option value={Shape.Round}>Okrągły</option>
                    <option value={Shape.Rectangular}>Prostokątny</option>
                </select>
            </td>
            <td></td>
            <td className="pizza-dough-suggested"></td>
        </tr>
    )
}


const PizzaDoughCalculator = () => {

    const [thicknessFactor, setThicknessFactor] = useState(0.123);
    const [doughBall, setDoughBall] = useState(3);
    const [diameter, setDiameter] = useState(31);
    const [length, setLength] = useState(31);
    const [width, setWidth] = useState(31);
    const [amountOfWater, setAmountOfWater] = useState(70);
    const [yeast, setYeast] = useState(0.5);
    const [salt, setSalt] = useState(2);
    const [oil, setOil] = useState(0);
    const [sugar, setSugar] = useState(0);
    const [ballResidue, setBallResidue] = useState(2);
    const [shape, setShape] = useState(Shape.Round);

    const getLehmannCoefficient = () => {
        return 4.393673111
    }

    const calcArea = () => {
        if (shape === Shape.Round) {
            return Math.PI * Math.pow(diameter / 2, 2)
        }
        else {
            return length * width;
        }

    }

    const calcDoughWeight = () => {
        return ((1 + ballResidue / 100) * (thicknessFactor * getLehmannCoefficient())) * calcArea()
    }

    const calcTotalPercentage = () => {
        return 100 / (amountOfWater + yeast + salt + oil + sugar + 100)
    }

    const calcFlour = () => {
        return doughBall * calcDoughWeight() * calcTotalPercentage()
    }

    const flourOutput = calcFlour();
    const waterOutput = calcFlour() * amountOfWater / 100;
    const saltOutput = calcFlour() * salt / 100;
    const yeastOutput = calcFlour() * yeast / 100;
    const sugarOutput = calcFlour() * sugar / 100;
    const oilOutput = calcFlour() * oil / 100;

    const calcTotal = () => {
        return flourOutput + waterOutput + saltOutput + yeastOutput + sugarOutput + oilOutput;
    }

    const getShapeControls = () => {
        if (shape === Shape.Round) {
            return (
                <PizzaSettingInput name="Średnica" unit="cm" onValueChanged={(v) => setDiameter(v)} defaultValue={31} />
            )
        }
        else {
            return (
                [
                    <PizzaSettingInput key="length" name="Długość" unit="cm" onValueChanged={(v) => setLength(v)} defaultValue={31} />,
                    <PizzaSettingInput key="width" name="Szerokość" unit="cm" onValueChanged={(v) => setWidth(v)} defaultValue={31} />
                ]

            )
        }
    }

    return (
        <div>
            <table>
                <tbody>
                    <tr className="pizza-dough-calculator__table-header">
                        <th className="pizza-dough-calculator__table-header--to-left">Ustawienie</th>
                        <th>Ilość</th>
                        <th>Jednostka</th>
                        <th>Sugerowane</th>
                    </tr>

                    <PizzaSettingInput name="Współczynnik grubości" onValueChanged={(v) => setThicknessFactor(v)} defaultValue={0.12} step={0.01} suggested="0.1 - 0.3" />
                    <PizzaSettingInput name="Ilość kul ciasta" onValueChanged={(v) => setDoughBall(v)} defaultValue={3} />
                    <PizzaSettingShapeSelect name="Kształt" onValueChanged={(v) => setShape(v)} />
                    {getShapeControls()}
                    <PizzaSettingInput name="Woda" unit="%" onValueChanged={(v) => setAmountOfWater(v)} defaultValue={70} suggested="58% - 65%" />
                    <PizzaSettingInput name="Drożdze" unit="%" onValueChanged={(v) => setYeast(v)} defaultValue={0.5} step={0.1} suggested="0.17% - 0.5%" />
                    <PizzaSettingInput name="Sól" unit="%" onValueChanged={(v) => setSalt(v)} defaultValue={2} suggested="0.5% - 3.0%" />
                    <PizzaSettingInput name="Olej" unit="%" onValueChanged={(v) => setOil(v)} defaultValue={0} suggested="opcjonalne 1% - 2%" />
                    <PizzaSettingInput name="Cukier" unit="%" onValueChanged={(v) => setSugar(v)} defaultValue={0} suggested="opcjonalne 1% - 2%" />
                    <PizzaSettingInput name="Pozostałość po cieście w misce" unit="%" onValueChanged={(v) => setBallResidue(v)} defaultValue={2} suggested="4%" />

                    <tr className="pizza-dough-calculator__table-header">
                        <th className="pizza-dough-calculator__table-header--to-left">Składnik</th>
                        <th></th>
                        <th></th>
                    </tr>

                    <PizzaRecipeOutput name="Mąka" value={flourOutput} unit="g" />
                    <PizzaRecipeOutput name="Woda" value={waterOutput} unit="g" />
                    <PizzaRecipeOutput name="Sól" value={saltOutput} unit="g" />
                    <PizzaRecipeOutput name="Drożdze" value={yeastOutput} unit="g" />
                    <PizzaRecipeOutput name="Cukier" value={sugarOutput} unit="g" />
                    <PizzaRecipeOutput name="Olej" value={oilOutput} unit="g" />

                    <tr className="pizza-dough-calculator__table-header">
                        <th className="pizza-dough-calculator__table-header--to-left">Podsumowanie</th>
                        <th></th>
                        <th></th>
                    </tr>

                    <PizzaRecipeOutput name="Suma" value={calcTotal()} unit="g" />
                    <PizzaRecipeOutput name="Jedna kula ciasta" value={calcTotal() / doughBall} unit="g" />
                </tbody>
            </table>
        </div>
    )
}

export default PizzaDoughCalculator;