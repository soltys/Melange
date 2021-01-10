import React, { useState } from 'react';
import { IconButton } from '../basic/Basic';
import './PizzaPriceComparison.css';

type PizzaPriceEntryProps = {
    id: number,
    onRemove: (id: number) => void
}

const PizzaPriceEntry: React.FunctionComponent<PizzaPriceEntryProps> = (props) => {
    const [diameter, setDiameter] = useState(45);
    const [price, setPrice] = useState(30);

    const calcAreaPrice = (): number => {
        const area = Math.PI * Math.pow(diameter / 2, 2);
        const value = price / area;
        if (isNaN(value) || !isFinite(value)) {
            return 0
        }
        return 100 * value;
    }
    return (
        <tr>
            <td><IconButton iconName="fas fa-trash-alt" onClick={(e) => props.onRemove(props.id)} /></td>
            <td><input type="number" onChange={(e) => setDiameter(e.target.valueAsNumber)} defaultValue={45} /></td>
            <td><input type="number" onChange={(e) => setPrice(e.target.valueAsNumber)} defaultValue={30} /></td>
            <td>{calcAreaPrice().toFixed(2)}</td>
            <td>{(price / 8).toFixed(2)}</td>
        </tr>

    )
}

const PizzaPriceComparison: React.FunctionComponent = (props) => {

    const [entries, setEntries] = useState<number[]>([1, 2])

    let onRemoveEntry = (id: number) => {
        const afterFilter = entries.filter((v) => {
            return v !== id;
        });

        setEntries(afterFilter);
    }
    const onAdd = () => {
        if (entries.length === 0) {
            setEntries([0])
            return
        }

        const lastId = entries[entries.length - 1]

        const newList = [...entries, lastId + 1]
        setEntries(newList)
    }


    return (
        <div className="pizza-price-comparison">
            <table>
                <thead>
                    <tr>
                        <th><IconButton iconName="fas fa-plus" onClick={(e) => onAdd()} /></th>
                        <th>Średnica cm</th>
                        <th>Cena zł</th>
                        <th>Cena za 100 cm<sup>2</sup></th>
                        <th>Cena za 1/8 pizzy</th>
                    </tr>
                </thead>
                <tbody>
                    {entries.map((e) => {
                        return (<PizzaPriceEntry key={e}
                            onRemove={(id) => onRemoveEntry(id)}
                            id={e} />)
                    })}
                </tbody>
            </table>
        </div>
    )
}

export default PizzaPriceComparison;