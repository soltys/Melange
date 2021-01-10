import React, { useState } from 'react';
import { ViewContainer, Box, BoxContainer, IconButton } from '../basic/Basic';
import "./FastFoodView.css"

interface Product {
    name: string,
    energy: number,
    fat: number,
    saturatedFat: number,
    carbohydrates: number,
    sugar: number,
    fiber: number,
    protein: number,
    salt: number
}

type DisplayProductProps = {
    product: Product
}

const DisplayProduct: React.FunctionComponent<DisplayProductProps> = (props) => {
    return (
        <React.Fragment>
            <td>{props.product.energy.toFixed(0)}</td>
            <td>{props.product.fat.toFixed(0)}</td>
            <td>{props.product.saturatedFat.toFixed(0)}</td>
            <td>{props.product.carbohydrates.toFixed(0)}</td>
            <td>{props.product.sugar.toFixed(0)}</td>
            <td>{props.product.fiber.toFixed(0)}</td>
            <td>{props.product.protein.toFixed(0)}</td>
            <td>{props.product.salt.toFixed(1)}</td>
        </React.Fragment>
    );


};

type ProductEntryProps = {
    products: Product[],
    product: Product,
    id: number,
    onRemove: (id: number | undefined) => void,
    onProductChanged: (id: number, product: Product) => void
}

const ProductEntry: React.FunctionComponent<ProductEntryProps> = (props) => {
    const names = props.products.map((p) => p.name);
    const handleProductChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const name = e.target.value;
        const selectedProduct = props.products.find((p) => p.name === name);
        if (selectedProduct) {
            props.onProductChanged(props.id, selectedProduct)
        }
    }
    return (
        <tr>
            <td><IconButton iconName="fas fa-trash-alt" onClick={(e) => props.onRemove(props.id)} /></td>
            <td>
                <select value={props.product.name} onChange={(e) => handleProductChange(e)} >
                    {names.map((n) => {
                        return <option key={n} value={n}>{n}</option>
                    })}
                </select>
            </td>
            <DisplayProduct product={props.product} />
        </tr>
    );
}
type ProductCalculatorProps = {
    productSource: Product[]
}
const ProductCalculator: React.FunctionComponent<ProductCalculatorProps> = (props) => {

    const [entries, setEntries] = useState<{ id: number, product: Product }[]>([
        {id:1, product:props.productSource[0]}
    ])
    const onProductChanged = (id: number, product: Product) => {
        const entryIdx = entries.findIndex((e, idx: number) => e.id === id);
        if (entryIdx >= 0) {
            entries[entryIdx].product = product
        }
        setEntries([...entries])
    }

    let onRemoveEntry = (id: number | undefined) => {
        const afterFilter = entries.filter((v) => {
            return v.id !== id;
        });

        setEntries(afterFilter);
    }
    const onAdd = () => {
        if (entries.length === 0) {
            const p = props.productSource[0];

            setEntries([{
                id: 1,
                product: p
            }])
            return
        }
        const newProduct = props.productSource[0];
        const lastProduct = entries[entries.length - 1];
        const newList = [...entries, {
            id: (lastProduct.id || 0) + 1,
            product: newProduct
        }]
        setEntries(newList);
    }
    type KeysOfType<T, TProp> = { [P in keyof T]: T[P] extends TProp ? P : never }[keyof T];
    const sumValues = (name: KeysOfType<Product, number>) => {
        let total = 0;

        entries.forEach((v) => {
            total += v.product[name]
        });
        return total;
    }

    return (
        <div>
            <table className="fast-food-calculator">
                <thead>
                    <tr>
                        <th><IconButton iconName="fas fa-plus" onClick={(e) => onAdd()} /></th>
                        <th>Nazwa</th>
                        <th>Energia [kcal]</th>
                        <th>Tłuszcze [g]</th>
                        <th>Tłuszcze nasycone [g]</th>
                        <th>Węglowodany [g]</th>
                        <th>Cukry [g]</th>
                        <th>Błonnik [g]</th>
                        <th>Białko [g]</th>
                        <th>Sól [g]</th>
                    </tr>
                </thead>
                <tbody>
                    {entries.map((e) => {
                        return (<ProductEntry key={e.id}
                            onProductChanged={(id, product) => onProductChanged(id, product)}
                            product={e.product}
                            products={props.productSource}
                            onRemove={(id) => onRemoveEntry(id)}
                            id={e.id} />)
                    })}
                </tbody>
                <tfoot>
                    <tr>
                        <td colSpan={2}>Podsumowanie</td>
                        <td>{sumValues("energy").toFixed(0)}</td>
                        <td>{sumValues("fat").toFixed(0)}</td>
                        <td>{sumValues("saturatedFat").toFixed(0)}</td>
                        <td>{sumValues("carbohydrates").toFixed(0)}</td>
                        <td>{sumValues("sugar").toFixed(0)}</td>
                        <td>{sumValues("fiber").toFixed(0)}</td>
                        <td>{sumValues("protein").toFixed(0)}</td>
                        <td>{sumValues("salt").toFixed(1)}</td>
                    </tr>
                </tfoot>
            </table>
        </div>
    )
}

const McDonaldDataSource = (): Product[] => {
    var data: Product[] = [

        {
            name: "Cheeseburger",
            energy: 302,
            fat: 12,
            saturatedFat: 6,
            carbohydrates: 31,
            sugar: 6.9,
            fiber: 1.9,
            protein: 16,
            salt: 1.6
        },
        {
            name: "Big Mac",
            energy: 503,
            fat: 25,
            saturatedFat: 9.7,
            carbohydrates: 42,
            sugar: 8.5,
            fiber: 3.1,
            protein: 26,
            salt: 2.2
        },
        {
            name: "McRoyal",
            energy: 521,
            fat: 27,
            saturatedFat: 14,
            carbohydrates: 37,
            sugar: 9.5,
            fiber: 2.2,
            protein: 32,
            salt: 2.6
        },
        {
            name: "WieśMac",
            energy: 581,
            fat: 37,
            saturatedFat: 12,
            carbohydrates: 33,
            sugar: 7.1,
            fiber: 2.5,
            protein: 30,
            salt: 1.9
        },
        {
            "name": "McWrap Klasyczny Chrupiący Kurczak",
            "energy": 486,
            "fat": 18,
            "saturatedFat": 4.7,
            "carbohydrates": 54,
            "sugar": 8.6,
            "fiber": 3.7,
            "protein": 24,
            "salt": 3.4
        },
        {
            "name": "McWrap Bekon Deluxe Chrupiący Kurczak",
            "energy": 586,
            "fat": 30,
            "saturatedFat": 5.4,
            "carbohydrates": 51,
            "sugar": 6.8,
            "fiber": 3.8,
            "protein": 26,
            "salt": 3.1
        },
        {
            name: "Podwójny McRoyal",
            energy: 761,
            fat: 45,
            saturatedFat: 22,
            carbohydrates: 36,
            sugar: 10,
            fiber: 2.3,
            protein: 52,
            salt: 3
        },
        {
            name: "Podwójny WieśMac",
            energy: 934,
            fat: 65,
            saturatedFat: 23,
            carbohydrates: 34,
            sugar: 8.2,
            fiber: 2.7,
            protein: 53,
            salt: 2.9
        },
        {

            name:"Frytki Małe",
            energy: 231,
            fat:11,
            saturatedFat:1,
            carbohydrates:29,
            sugar:0.3,
            fiber:2.8,
            protein:2.8,
            salt:0.55,
        },
        {

            name:"Frytki Średnie",
            energy: 330,
            fat:16,
            saturatedFat:1.5,
            carbohydrates:41,
            sugar:0.4,
            fiber:4,
            protein:3.9,
            salt:0.79,
        },
        {

            name:"Frytki Duże",
            energy: 434,
            fat:21,
            saturatedFat:2,
            carbohydrates:54,
            sugar:0.5,
            fiber:5.3,
            protein:5.2,
            salt:1,
        },
        {
            name: "Coca-Cola Mała",
            energy: 106,
            fat: 0,
            saturatedFat: 0,
            carbohydrates: 26,
            sugar: 26,
            fiber: 0,
            protein: 0,
            salt: 0
        },
        {
            name: "Coca-Cola Średnia",
            energy: 170,
            fat: 0,
            saturatedFat: 0,
            carbohydrates: 42,
            sugar: 42,
            fiber: 0,
            protein: 0,
            salt: 0
        },
        {
            name: "Coca-Cola Duża",
            energy: 213,
            fat: 0,
            saturatedFat: 0,
            carbohydrates: 53,
            sugar: 53,
            fiber: 0,
            protein: 0,
            salt: 0
        },
    ];

    return data;
}

const FastFoodView = () => {
    return (
        <ViewContainer title="Fast Food">
            <h1>Fast Food</h1>
            <BoxContainer>
                <Box>
                    <h2>McDonald</h2>
                    <ProductCalculator productSource={McDonaldDataSource()} />
                </Box>
            </BoxContainer>

        </ViewContainer>
    );
};


export default FastFoodView;
