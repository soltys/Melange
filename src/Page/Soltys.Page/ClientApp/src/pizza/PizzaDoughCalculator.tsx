import React, { useState } from 'react';
import './PizzaDoughCalculator.css';
import { IconButton } from '../basic/Basic';

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
                defaultValue={props.defaultValue} onChange={(e) => onValueChanged(e)} min="0" step={props.step} value={props.defaultValue} /></td>
            <td>{props.unit}</td>
            <td className="pizza-dough-suggested">{props.suggested}</td>
        </tr>
    )
}

type PizzaRecipeOutputProps = {
    name: string,
    value: number,
    unit?: string,
    precision?: number
}
const PizzaRecipeOutput: React.FunctionComponent<PizzaRecipeOutputProps> = (props) => {
    const precision = props.precision ?? 0;
    return (
        <tr>
            <td>{props.name}</td>
            <td className="pizza-dough-calculator__output">{props.value.toFixed(precision)}</td>
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
    defaultValue: Shape,
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
                <select className="pizza-dough-calculator__input" defaultValue={props.defaultValue} value={props.defaultValue} onChange={(v) => onValueChanged(v)}>
                    <option value={Shape.Round}>Okrągły</option>
                    <option value={Shape.Rectangular}>Prostokątny</option>
                </select>
            </td>
            <td></td>
            <td className="pizza-dough-suggested"></td>
        </tr>
    )
}

type PizzaDougCalculatorInputData = {
    thicknessFactor: number,
    doughBall: number,
    diameter: number,
    length: number,
    width: number,
    amountOfWater: number,
    yeast: number,
    salt: number,
    oil: number,
    sugar: number,
    ballResidue: number,
    shape: Shape
}

type PizzaDoughSaverProps = {
    currentInput: PizzaDougCalculatorInputData,
    onSettingsRestored: (val: PizzaDougCalculatorInputData) => void,
}

interface SavedPizzaDoughEntry {
    name: string,
    data: PizzaDougCalculatorInputData
}

const usePizzaDoughStorage = (localStorageKey: string): [SavedPizzaDoughEntry[], React.Dispatch<React.SetStateAction<SavedPizzaDoughEntry[]>>] => {
    const storage = window.localStorage;
    const [value, setValue] = React.useState<SavedPizzaDoughEntry[]>(() => {
        const savedData = storage.getItem(localStorageKey);
        let savedDoughs: SavedPizzaDoughEntry[] = [];
        if (savedData !== null) {
            savedDoughs = JSON.parse(savedData) as SavedPizzaDoughEntry[];
            if (!Array.isArray(savedDoughs)) {
                savedDoughs = [];
            }
        }
        return savedDoughs;
    });

    React.useEffect(() => {
        storage.setItem('pizzaDough', JSON.stringify(value));
    }, [value, storage]);

    return [value, setValue];
};

const PizzaDoughSaver: React.FunctionComponent<PizzaDoughSaverProps> = (props) => {

    const [doughName, setDoughName] = useState("");
    const [savedPizzaDoughs, setSavedPizzaDoughs] = usePizzaDoughStorage('pizzaDough');
    const [errorMessage, setErrorMessage] = useState("");

    const saveInput = () => {

        setErrorMessage("");



        if(doughName === "" || doughName === null){
            setErrorMessage("Nazwa nie może być pusta")
            return;
        }

        const sameNameExisting = savedPizzaDoughs.filter((v) => {
            return v.name === doughName;
        });

        if (sameNameExisting.length > 0) {
            setErrorMessage("Nazwy muszą być unikalne. Wybierz inną nazwę lub usuń starą.")
            return;
        }

        const tmp = savedPizzaDoughs;
        const newValue = [...tmp, {
            name: doughName,
            data: props.currentInput,
        }];
        setDoughName("");
        setSavedPizzaDoughs(newValue);
    };

    const handleKeyDown = (e: React.KeyboardEvent) => {
        if (e.key === 'Enter') {
            saveInput();
        }
    }

    const onDoughNameChanged = (e: React.ChangeEvent<HTMLInputElement>) => {
        const val = e.target.value;
        if (val === null || val.match(/^ *$/) !== null) {
            setDoughName("");
        }
        else {
            setDoughName(val);
        }
    }

    const removeInput = (name: string) => {
        const afterFilter = savedPizzaDoughs.filter((v) => {
            return v.name !== name;
        });

        setSavedPizzaDoughs(afterFilter);
    }

    const restoreInput = (val: PizzaDougCalculatorInputData) => {
        props.onSettingsRestored(val);
    }

    const showDoughSaved = () => {
        if (savedPizzaDoughs.length === 0) {
            return (<></>)
        }
        else {
            return (
                <div>
                    <h4>Wcześniej zapisane</h4>
                    <table>
                        {savedPizzaDoughs.map((entry) => {
                            return (<tr key={entry.name}>
                                <td>
                                    <IconButton iconName="fas fa-trash-alt" onClick={(e) => removeInput(entry.name)} />
                                </td>
                                <td>
                                    <button onClick={() => restoreInput(entry.data)}>{entry.name}</button>
                                </td>
                            </tr>)
                        })}
                    </table>
                </div>
            )
        }
    }

    const showErrorMessage = () => {
        if (errorMessage !== "") {
            return (
                <p><strong>{errorMessage}</strong></p>
            )
        }
        else {
            return (<></>);
        }
    }


    return (
        <div className="pizza-dough-save">
            <h3>Zapisz <abbr title="Wykorzystuje JavaScriptowy LocalStorage">(w danych przeglądarki)</abbr></h3>
            <div>
                <label htmlFor="savedName">Nazwa</label>
                <input type="text" name="savedName" onChange={(v) => onDoughNameChanged(v)} onKeyDown={(v) => handleKeyDown(v)} value={doughName} />
                <IconButton iconName="fas fa-plus" onClick={(e) => saveInput()} />
                {showErrorMessage()}
            </div>
            {showDoughSaved()}
        </div>)
};

const PizzaDoughCalculator = () => {

    const [thicknessFactor, setThicknessFactor] = useState(0.11);
    const [doughBall, setDoughBall] = useState(3);
    const [diameter, setDiameter] = useState(31);
    const [length, setLength] = useState(31);
    const [width, setWidth] = useState(31);
    const [amountOfWater, setAmountOfWater] = useState(65);
    const [yeast, setYeast] = useState(0.3);
    const [salt, setSalt] = useState(2);
    const [oil, setOil] = useState(0);
    const [sugar, setSugar] = useState(0);
    const [ballResidue, setBallResidue] = useState(2);
    const [shape, setShape] = useState(Shape.Round);

    const input: PizzaDougCalculatorInputData = {
        thicknessFactor: thicknessFactor,
        doughBall: doughBall,
        diameter: diameter,
        length: length,
        width: width,
        amountOfWater: amountOfWater,
        yeast: yeast,
        salt: salt,
        oil: oil,
        sugar: sugar,
        ballResidue: ballResidue,
        shape: shape,
    };

    const onPizzaDoughValueRestored = (val: PizzaDougCalculatorInputData) => {
        setThicknessFactor(val.thicknessFactor);
        setDoughBall(val.doughBall);
        setDiameter(val.diameter);
        setLength(val.length);
        setWidth(val.width);
        setAmountOfWater(val.amountOfWater);
        setYeast(val.yeast);
        setSalt(val.salt);
        setOil(val.oil);
        setSugar(val.sugar);
        setBallResidue(val.ballResidue);
        setShape(val.shape);
    }


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
                <PizzaSettingInput name="Średnica" unit="cm" onValueChanged={(v) => setDiameter(v)} defaultValue={diameter} />
            )
        }
        else {
            return (
                [
                    <PizzaSettingInput key="length" name="Długość" unit="cm" onValueChanged={(v) => setLength(v)} defaultValue={length} />,
                    <PizzaSettingInput key="width" name="Szerokość" unit="cm" onValueChanged={(v) => setWidth(v)} defaultValue={width} />
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

                    <PizzaSettingInput name="Współczynnik grubości" onValueChanged={(v) => setThicknessFactor(v)} defaultValue={thicknessFactor} step={0.01} suggested="0.1 - 0.3" />
                    <PizzaSettingInput name="Ilość kul ciasta" onValueChanged={(v) => setDoughBall(v)} defaultValue={doughBall} />
                    <PizzaSettingShapeSelect name="Kształt" onValueChanged={(v) => setShape(v)} defaultValue={shape} />
                    {getShapeControls()}
                    <PizzaSettingInput name="Woda" unit="%" onValueChanged={(v) => setAmountOfWater(v)} defaultValue={amountOfWater} suggested="58% - 65%" />
                    <PizzaSettingInput name="Drożdże" unit="%" onValueChanged={(v) => setYeast(v)} defaultValue={yeast} step={0.1} suggested="0.17% - 0.5%" />
                    <PizzaSettingInput name="Sól" unit="%" onValueChanged={(v) => setSalt(v)} defaultValue={salt} suggested="0.5% - 3.0%" />
                    <PizzaSettingInput name="Olej" unit="%" onValueChanged={(v) => setOil(v)} defaultValue={oil} suggested="opcjonalne 1% - 2%" />
                    <PizzaSettingInput name="Cukier" unit="%" onValueChanged={(v) => setSugar(v)} defaultValue={sugar} suggested="opcjonalne 1% - 2%" />
                    <PizzaSettingInput name="Pozostałość po cieście w misce" unit="%" onValueChanged={(v) => setBallResidue(v)} defaultValue={ballResidue} suggested="opcjonalne 2% - 4%" />

                    <tr className="pizza-dough-calculator__table-header">
                        <th className="pizza-dough-calculator__table-header--to-left">Składnik</th>
                        <th></th>
                        <th></th>
                    </tr>

                    <PizzaRecipeOutput name="Mąka" value={flourOutput} unit="g" />
                    <PizzaRecipeOutput name="Woda" value={waterOutput} unit="g" />
                    <PizzaRecipeOutput name="Sól" value={saltOutput} unit="g" />
                    <PizzaRecipeOutput name="Drożdze" value={yeastOutput} unit="g" precision={2} />
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

            <PizzaDoughSaver currentInput={input} onSettingsRestored={onPizzaDoughValueRestored} />

        </div>
    )
}

export default PizzaDoughCalculator;