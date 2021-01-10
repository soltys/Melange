
import React, { useState } from 'react';
import './PizzaHutCalculator.css';

export const PizzaHutPrices = () => {
    return (
        <div>
            Przykładowe ceny:
            <ul>
                <li>Peperoni duża; san francisco;	35.99 </li>
                <li>Super supreme; duża; pan	44.99</li>
                <li>Europejska duża; san francisco;	39.99</li>
                <li>Farmerska; duża; san francisco;	40.99</li>
                <li>Peperoni; średnia; san francisco;	26.99</li>
                <li>Super supreme; średnia; pan;	34.99</li>
            </ul>
        </div>
    )
}

export const PizzaHutCalculator = () => {

    const [firstPizza, setFirstPizza] = useState(26.99);
    const [secoundPizza, setSecoundPizza] = useState(34.99);
    const calcSum = (): number => {
        return (firstPizza + secoundPizza);
    }

    const calcPercentage = (pizzaValue: number) => {
        return ((pizzaValue * 100) / calcSum());
    }

    const calcSumAfterPromotion = (): number => {
        return Math.max(firstPizza, secoundPizza) + 1;
    }

    const calcPriceToPay = (pizzaValue: number): number => {
        return calcSumAfterPromotion() * (calcPercentage(pizzaValue) / 100)
    }

    return (
        <table className="pizzahut-calculator">
            <thead>
                <tr>
                    <th></th>
                    <th>zł</th>
                    <th>%</th>
                    <th></th>
                    <th>Kwota do zapłaty</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Pierwsza pizza</td>
                    <td><input type="number" onChange={(e) => setFirstPizza(e.target.valueAsNumber)} className="pizzahut-calculator__input" defaultValue={26.99} /></td>
                    <td>{calcPercentage(firstPizza).toFixed(0)}</td>
                    <td></td>
                    <td><strong>{calcPriceToPay(firstPizza).toFixed(2)}</strong></td>
                </tr>
                <tr>
                    <td>Druga pizza</td>
                    <td><input type="number" onChange={(e) => setSecoundPizza(e.target.valueAsNumber)} defaultValue={34.99} /></td>
                    <td>{calcPercentage(secoundPizza).toFixed(0)}</td>
                    <td></td>
                    <td><strong>{calcPriceToPay(secoundPizza).toFixed(2)}</strong></td>
                </tr>
                <tr>
                    <td>Suma</td>
                    <td>{calcSum().toFixed(2)}</td>
                    <td>100</td>
                    <td>2-ga za 1zł</td>
                    <td>{calcSumAfterPromotion().toFixed(2)}</td>
                </tr>
            </tbody>
        </table>
    )
}


export default PizzaHutCalculator;