describe('TA 2', ()=> {

    before(() => {
        cy.login()
        cy.saveLocalStorage();
      });
  
      beforeEach(() => {
        cy.visit("/");
        cy.restoreLocalStorage();
      });
 
  
  const WSC_URL_DEV = Cypress.config("baseUrl");//"http://localhost:4200/main/home";
  const STR_VERSION=Cypress.config("version_to_check");//" v #{version}# ";//" v 0.2.0716.8 ";
  it('Should login succesfully and show the welcome message', () => { 
      // Visita al portal
      cy.visit(WSC_URL_DEV)
      
      // Verificando existencia del titulo en el home
      cy.get('.page-title').should('have.text', 'Bienvenido a WSC')
  
  })
  
  
   
  it('Should login succesfully and show the environment name', () => { 
    // Visita al portal
    
    // Verificando existencia del titulo en el home
    cy.get('.title-environment.ng-star-inserted').should('have.text', Cypress.config("K8Enviroment"))
  })
  
  



  it('Should login succesfully and show the web app version', () => { 
    // Visita al portal
    cy.visit("/")
    
    // Verificando existencia del titulo en el home
    cy.get('.title-version')
      .should('have.text', STR_VERSION )
})

it('Should login succesfully, unfold the side menu and fold it again', () => { 
    // Visita al portal
    cy.visit("/")

    cy.wait(2000)

    // Desplegando el menú lateral
    cy.get('[src="assets/menu/menu.svg"]')
      .click()

    cy.wait(2000)

    // Plegando el menú lateral
    cy.get('[src="assets/menu/active.svg"]')
      .click()
})

it('Should login and go to the Volumen Baseline home', () => { 
    // Visita al portal
    cy.visit(WSC_URL_DEV)

    cy.wait(2000)

    // Desplegando el menú lateral
    cy.get('[src="assets/menu/fuentes.svg"]')
      .click()

    cy.wait(2000)

    // Plegando el menú lateral
    cy.get('[tabindex="0"]')
      .click()
})









  })