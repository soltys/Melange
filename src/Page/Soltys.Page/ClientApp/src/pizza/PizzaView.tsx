import React from 'react';
import { ViewContainer, BoxContainer, Box } from '../basic/Basic';
import PizzaDoughCalculator from './PizzaDoughCalculator';
import { PizzaHutCalculator, PizzaHutPrices } from './PizzaHutCalculator';
import PizzaPriceComparison from './PizzaPriceComparison';
import './PizzaView.css';
const PizzaView = () => {
    return (
        <ViewContainer title="Pizza">
            <h1>Pizza</h1>
            <BoxContainer>
                <Box>
                    <h2>Kalkulator ciasta do pizzy</h2>
                    <PizzaDoughCalculator />
                </Box>
                <Box>
                    <div className="pizza-description">
                        <h2>O kalkulatorze:</h2>
                        <p>Oryginalnie idea kalkulatora została ściągnięta ze strony <a href="https://www.pizzamaking.com/dough-calculator.html">PizzaMaking</a>. Kalkulator jest napisany we flashu, a przeglądarki od niego odchodzą. Chcąc ratować ten wspaniały twór, odtworzyłem go w Reactcie.</p>
                        <p>Kalkulator wylicza ilość ciasta dla podanego współczynnika grubości. Ten współczynnik jest wartością bez miarową. 0.1 daje super-cienkie ciasto. Osobiście najbardziej lubię 0.123.</p>
                        <p>Poziom hydracji ciasta powinien być z zakresie 60% &#8211; 70%, im więcej wody tym ciasto jest bardziej chrupkie, ale trudniejsze w obsłudze, ponieważ jest bardziej płynne i klei się do wszystkiego.  Zalecam zacząć od 60% i potem stopniowo iść w górę, aż nabierzemy wprawy.</p>
                        <p><strong>Kolejnym ważnym elementem jest rodzaj mąki.</strong> Osobiście polecam wybrać <strong>typu</strong> <strong>550</strong>, każda mąka ma inny stosunek absorbcji wody, tak więc będzie się inaczej zachowywać.</p>
                        <p>Rodzaj drożdży który wykorzystuje to drożdże instant w proszku. Te piekarskie w kostce zazwyczaj się zepsują zanim człowiek wykorzysta wszystko.</p>
                    </div>
                </Box>
                <Box>
                    <div className="pizza-description">
                        <h2>Jak to zrobić?</h2>
                        <p>Do letniej wody, 25 stopni (20 sekund w mojej mikrofali) wsypuję suche drożdże. Dodaje też szczyptę cukru i mieszam. Do miski przez sitko wsypuję mąkę oraz sól.</p>
                        <p>Wlewam wodę z drożdżami. Mieszam ręką aż wszystko się połączy w jedno.</p>
                        <p>Po połączeniu wszystkiego wyrzucam z miski ciasto na stolicę. Potem przykrywam ciasto miską, w której mieszałem i zostawiam ciasto same sobie na 1 godzinę.</p>
                        <p>Potem dodając trochę mąki lekko wyrabiam ciasto, ale bez przesady. Ciasto ma się kleić do siebie bardziej niż do ręki. Nie można wtłoczyć zbyt dużej ilości mąki.</p>
                        <p>Dzielę ciasto na X porcji (patrz kalkulator), miski na ciasto smaruję oliwą z oliwek, tworzę kulę do osobnych misek i przykrywam folią spożywczą. Ciasto idzie do lodówki na <strong>minimum 24 godziny.</strong> Ciasto w lodówce może wytrzymać nawet do 72 godzin bez złej fermentacji.</p>
                        <p>Formę do pieczenia posypuję mąką ciasto od góry też sypię mąką. Od odklejam delikatnie ciasto z miski tak aby nie wybić powietrza z ciasta. I tak aby był kontakt mąka-mąka.</p>
                        <p>Rozciągam rękoma ciasto do brzegów formy.</p>
                        <p>Rozgrzej piekarnik 275 C z czymś co ma robić za kamień do pizzy, jeżeli go nie masz np. patelnia żeliwna.</p>
                        <p>Czas pieczenia 7-8 minut, lub do złotego ciasta i gdzie ser jest brązowawy. </p>
                    </div>
                </Box>
            </BoxContainer>


            <h2>Pizza hut (2-ga pizza za 1zł)</h2>
            <BoxContainer>
                <Box>
                    <PizzaHutCalculator />
                </Box>
                <Box>
                    <PizzaHutPrices />
                </Box>
            </BoxContainer>


            <h2>Porównywarka cen pizzy (średnica/cena)</h2>
            <BoxContainer>
                <Box>
                    <PizzaPriceComparison />
                </Box>
            </BoxContainer>

        </ViewContainer >
    )
}

export default PizzaView
