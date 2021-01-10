import React from 'react';

import { ViewContainer, BoxContainer, Box, RecipeList } from '../basic/Basic'
import RatioCalculator from './RatioCalculator';
import './CoffeeView.css';
import {
    Link
} from "react-router-dom";

const CoffeeView = () => {
    return (
        <ViewContainer title="Kawa">
            <h1>Kawa</h1>

            <BoxContainer>
                <Box>
                    <h2>Kalkulator proporcji kawy</h2>
                    <RatioCalculator unit="g" inputDescription="kawy" outputDescription="ziaren kawy" />
                </Box>
                <Box>
                    <h2>Przepisy Areopress</h2>


                    <h3>Normalny kubek czarnej kawy</h3>
                    <RecipeList>
                        <li>Ustaw AreoPress w pozycji normalnej, przygotuj 2 namoczone filtry papierowe do Areopressu</li>
                        <li>Zmiel <strong>15g</strong> kawy (Comendate 25-30 kilk), którą wsyp do areopresu</li>
                        <li>Zalej <strong>225g</strong> wody i załóż tłok, aby powstała próżnia</li>
                        <li>Po 1 minucie zdejmij tłok i lekko <strong>przełam pianę łyżką</strong>, potem załóż tłok ponownie</li>
                        <li>Po 4 minutach od zalania przecisnij kawę w powolny sposób aż usłyszysz syczenie</li>
                        <li><a href="https://www.thecoffeecompass.com/the-only-aeropress-recipe-youll-ever-need/">autor</a></li>
                    </RecipeList>

                    <h3><em>Espresso-like</em> z Fellow Prismo</h3>
                    <RecipeList>
                        <li>Ustaw AreoPress w pozycji normalnej, przygotuj <a href="https://www.coffeedesk.pl/product/5712/Fellow-Prismo"><strong>Fellow Prismo</strong></a></li>
                        <li>Zmiel kawę jak do Espresso; <strong>18g kawy</strong> (Comendate - 12 klików)</li>
                        <li>Rozgrzej AreoPress wrzątkiem, wylej wodę</li>
                        <li>Dodaj zmieloną kawę, potrząśnij areopresem aby wyrównać kawę, ubij kawę tamperem</li>
                        <li>Zalej kawę <strong>55 ml wrzątku</strong>, odczekaj 30 sekund</li>
                        <li>Przeciśnij tłok z całą siłą</li>
                    </RecipeList>
                </Box>
                <Box>
                    <h2>Przepisy</h2>
                    <h3>French press</h3>
                    <RecipeList>
                        <li>Użyj proporcji <strong>1:17</strong>; średni poziom zmielenia (Comendate 30 kilk)</li>
                        <li>Dodaj wodę, nie mieszaj, zaparzaj kawę przez <strong>4 min</strong></li>
                        <li>Po 4 minutach <strong>przełam pianę</strong>, wyrzuć wszystko co pływa</li>
                        <li>Poczekaj następne <strong>5-8 min</strong></li>
                        <li>Nie wyciskaj tłoku do samego końca, użyj go jako filter, umieszczając go na powieszchni kawy.</li>
                    </RecipeList>

                    <h3>Cold Brew</h3>
                    <RecipeList>
                        <li><strong>70g</strong> kawy zmielonej dość grubo (Comendate 40-45 kilk)</li>
                        <li><strong>1 litr</strong> wody</li>
                        <li>Czas parzenia <strong>9-12 godzin</strong> w lodówce</li>
                        <li>Filtrowanie np. przez filtr od kawy</li>
                    </RecipeList>
                </Box>
                <Box>
                    <h2>Ustawienia młynków</h2>
                    <h3>Comendate C40 MK3 (ilość klików)</h3>
                    <RecipeList>
                        <li>Espresso: 10-15</li>
                        <li>Kawiarka: 15-20</li>
                        <li>Areopress: 20-32</li>
                        <li>French Press: 25-32</li>
                        <li>Cold-Brew: 40-45</li>
                    </RecipeList>

                    <h3>Hario Mini Mill Slim Plus (ilość klików)</h3>
                    <RecipeList>
                        <li>Areopress: 6-8</li>
                        <li>French Press: 12-14</li>
                    </RecipeList>
                </Box>
            </BoxContainer>

            <div className="coffee-meme-link">
                <Link to="/coffee-meme"><span role="img" aria-label="smilling face">😄</span></Link>
            </div>
        </ViewContainer>
    )
};

export default CoffeeView
