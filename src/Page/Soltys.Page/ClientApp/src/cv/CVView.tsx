import React from 'react';
import './CVView.css';
import { SkillList, Skill } from './Skill';
import Section from './Section'

import Picture from './Picture';

const CVView = () => {
    return (        
        <div className="cv-view">
            <div className="cv-print-notice">
                <div className="cv-print-notice--message">
                    Strona specjalnie przygotowana pod wydruk
                    lub do eksportu PDFa przez przeglądarkę :)
                </div>
            </div>

            <CVPage>
                <Header />

                <Section header="O mnie">
                    <div className="about-me__description">
                        <p>Jestem programistą z wielką pasją do technologii. Uważam, że oprogramownie jest potrzebne w każdej dziedzinie życia i sądzę, że ja mogę to oprogramowanie stworzyć.</p>
                        <p>Pracowałem przy projektach na wielu platformach Mobile, Web oraz Windows. Nigdy nie zamykam się na jedną technologę i jestem otwarty na nowe wyzwania.</p>
                    </div>
                </Section>

                <Section header="Doświadczenie zawodowe">
                    <Job title="DGS Bussiness Services" workingYears="2015.12 - obecnie">
                        <Project name="Software Solutions">
                            <p>Rozwój i utrzymanie oprogramowania umożwiającego dopasowanie aparatu słuchowego dla klienta. Aplikacja na system operacyjny Windows wykorzystująca <strong>MEF</strong> w celu tworzania wielu edycji oprogramowania w jednym wydaniu</p>
                            <p>Integracja z urządzeniami służącymi do wykonywania Real Ear Measurment (w skrócie REM).</p>
                            <p>Technologie z którymi pracowałem: <strong>WPF</strong>, MEF, PRISM.</p>
                        </Project>
                    </Job>
                    <Job title="ObjectConnect" workingYears="2012.05 - 2015.11">
                        <Project name="Single Page Application">
                            <p>Stworzenie aplikacji webowej dla europejskiego producenta elektroniki samochodowej. Aplikacja zastępowała istniejcą
                                aplikację.</p>
                            <p>Technologie z którymi pracowałem: REST Service, <strong>NHibernate</strong>, <strong>Ember.js</strong></p>
                        </Project>
                        <Project name="METROpoint">
                            <p>Projekt i rozwój oprogramownia na platformę Windows 8, umożliwającą na dostęp do danych z platformy SharePoint. Aplikacja
            zdobyła <strong>nagrodę</strong> na European SharePoint Conference w 2013r.</p>
                            <p>Technologie z którymi pracowałem: Windows Runtime, <strong>Reactive Extensions</strong>, SQLite.</p>
                        </Project>
                    </Job>
                </Section>

                <Section header="Umiejętności">
                    <SkillList>
                        <Skill name="C#" />
                        <Skill name="Visual Studio" />
                        <Skill name="Windows" />
                        <Skill name="Angielski biegły" />
                        <Skill name="TypeScript" />
                        <Skill name="Git" />
                        <Skill name="Unit testing" />
                        <Skill name="Prawo jazdy kat. B" />
                        <Skill name="WPF" />
                        <Skill name="React" />
                        <Skill name="Go" />
                        <Skill name="Linux" />
                    </SkillList>
                </Section>
            </CVPage>

            <CVPage>
                <Section header="Edukacja">
                    <Education
                        schoolName="Zachodniopomorski Uniwerstytet Technologiczny w Szczecinie"
                        description="Informatyka" />
                </Section>

                <Section header="Inne">
                    <p><span className="other-entry">Zainteresowania</span>: eSport, Amerykańska Polityka</p>
                    <p><span className="other-entry">Blog</span>: <a href="https://blog.soltysiak.it"> o programowaniu, polityce i gotowaniu</a></p>
                    <p>
                        <span className="me-in-internet">
                            <a href="http://twitter.com/soltys" title="Twitter" target="_blank" rel="noopener noreferrer" aria-label="twitter">
                                <i className="fab fa-twitter" aria-hidden="true"></i></a>
                            <a href="https://www.facebook.com/pawel.soltys.soltysiak" title="Facebook" target="_blank" rel="noopener noreferrer" aria-label="facebook">
                                <i className="fab fa-facebook" aria-hidden="true"></i></a>
                            <a href="https://pl.linkedin.com/in/psoltysiak" title="LinkedIn" target="_blank" rel="noopener noreferrer" aria-label="linkedin">
                                <i className="fab fa-linkedin" aria-hidden="true"></i></a>
                            <a href="http://github.com/soltys" title="Github" target="_blank" rel="noopener noreferrer" aria-label="github">
                                <i className="fab fa-github-alt" aria-hidden="true"></i></a>
                        </span>
                    </p>
                </Section>


                <Section header="Zgoda na przetwarzanie danych osobowych">
                    <p>Wyrażam zgodę na przetwarzanie moich danych osobowych w celu rekrutacji zgodnie z art. 6 ust. 1 lit. a Rozporządzenia Parlamentu Europejskiego i Rady (UE) 2016/679 z dnia 27 kwietnia 2016 r. w sprawie ochrony osób fizycznych w związku z przetwarzaniem danych osobowych i w sprawie swobodnego przepływu takich danych oraz uchylenia dyrektywy 95/46/WE (ogólne rozporządzenie o ochronie danych)</p>
                </Section>
            </CVPage>


        </div>
    )
}

const CVPage: React.FunctionComponent<{}> = (props) => {
    return (
        <div className="cv-page">
            <div className="cv-page__content">
                {props.children}
            </div>
        </div>
    );
}

const Header = () => {
    return (
        <div className="header">
            <Picture />

            <div className="header-info">
                <h1>Paweł Sołtysiak</h1>
                <h2>Programista</h2>
            </div>

            <div className="contact-info">
                <ul>
                    <li><strong>Szczecin</strong></li>
                    <li>e-mail: <code>pawel&#8203;@soltysiak.it</code></li>
                    <li>tel: na żądanie</li>
                </ul>
            </div>
        </div>
    )
};




const Education: React.FunctionComponent<{ schoolName: string, description: string }> = (props) => {
    return (
        <div className="education">
            <h3>{props.schoolName}</h3>
            <h4>{props.description}</h4>
        </div>
    )
}

const Job: React.FunctionComponent<{ title: string, workingYears: string }> = (props) => {
    return (
        <div className="job">
            <div className="job-info">
                <h3 className="job-title">{props.title}</h3>
                <h4 className="job-time working-years">{props.workingYears}</h4>
            </div>
            {props.children}
        </div>
    );
}

const Project: React.FunctionComponent<{ name: string }> = (props) => {
    return (
        <div>
            <h4>{props.name}</h4>
            {props.children}
        </div>
    )
}


export default CVView;
