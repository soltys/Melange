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
                    <p>Page designed to print to PDF :) </p>
                    <p>Please use A4 format.</p>
                </div>
            </div>

            <CVPage>
                <div className="header">
                    <Picture />

                    <div className="header-info">
                        <h1>Paweł Sołtysiak</h1>
                        <h2>DevOps/Senior Software Developer</h2>
                    </div>

                    <div className="contact-info">
                        <ul>
                            <li><strong>Szczecin</strong></li>
                            <li>e-mail: <code>pawel&#8203;@soltysiak.it</code></li>
                            <li>phone: contact by mail, please</li>
                        </ul>
                    </div>
                </div>

                <Section header="About me">
                    <div className="about-me__description">
                        <p>I would describe myself as passionate developer with love for software. In my opinion every area of life would benefit from <i>a little bit of</i> software. I am certain I can create it.</p>
                        <p>I worked on various projects including DevOps, Windows, Web and Mobile. I do not focus on one technology, but I choose the best tool for the job. I am looking for new challenges in life.</p>
                    </div>
                </Section>

                <Section header="Work experience">
                    <Job title="Demant" workingYears="2015.12 - until now">
                        <Project name="Software Solutions - DevOps">
                            <p>Responsible for keeping Integration pipeline working. Developing and support process for gathering data about used third party libraries.</p>
                            <p>Technology stack: <strong>PowerShell</strong>, Azure DevOps.</p>
                        </Project>
                        <Project name="Software Solutions">
                            <p>Developing and support software responsible for adjusting hearing aid for client. Application written for Windows operating system.</p>
                            <p>Integration with companies making Real Ear Measurement devices. Developing feature allowing automated measurement and Hearing Aid adjustment using IMC2 protocol.</p>
                            <p>Technology stack: <strong>WPF</strong>, PRISM, Net Framework.</p>
                        </Project>
                    </Job>
                    <Job title="ObjectConnect" workingYears="2012.05 - 2015.11">
                        <Project name="Single Page Application">
                            <p>Developing web application for european manufacture of car automotive. Application was a drop-in replacement for existing one.</p>
                            <p>Technology stack: REST, <strong>NHibernate</strong>, <strong>Ember.js</strong></p>
                        </Project>
                        <Project name="METROpoint">
                            <p>Design and development of application for Windows 8 RT platform, enabling user to access SharePoint data. Application won <strong>an award</strong> on European SharePoint Conference in 2013 year.</p>
                            <p>Technology stack: Windows Runtime, <strong>Reactive Extensions</strong>, SQLite.</p>
                        </Project>
                    </Job>
                </Section>
            </CVPage>

            <CVPage>
                <Section header="Skills">
                    <SkillList>
                        <Skill name="C#" />
                        <Skill name="Visual Studio (and Code)" />
                        <Skill name="Windows" />
                        <Skill name="TypeScript" />
                        <Skill name="Git" />
                        <Skill name="Unit testing" />
                        <Skill name="Driving licence cat. B" />
                        <Skill name="WPF" />
                        <Skill name="React" />
                        <Skill name="PowerShell" />
                        <Skill name="Linux" />
                        <Skill name="Go" />
                    </SkillList>
                </Section>


                <Section header="Education">
                    <Education
                        schoolName="Zachodniopomorski Uniwerstytet Technologiczny w Szczecinie"
                        description="Computer Science" />
                </Section>

                <Section header="Other">
                    <p><span className="other-entry">Interests</span>: Programming (automation of daily tasks), Coffee, Italian Cuisine</p>
                    <p><span className="other-entry">Blog</span>: <a href="https://blog.soltysiak.it"> about programming, politics and cooking</a></p>
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


                <Section header="">
                    <p>I agree to the processing of personal data provided in this document for realising the recruitment process pursuant to the Personal Data Protection Act of 10 May 2018 (Journal of Laws 2018, item 1000) and in agreement with Regulation (EU) 2016/679 of the European Parliament and of the Council of 27 April 2016 on the protection of natural persons with regard to the processing of personal data and on the free movement of such data, and repealing Directive 95/46/EC (General Data Protection Regulation).</p>
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
