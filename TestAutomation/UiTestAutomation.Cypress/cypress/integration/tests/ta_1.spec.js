describe('TA 1', ()=> {

  before(() => {
    cy.clearLocalStorageSnapshot();
  });


beforeEach(()=> {

  cy.login()

})

const WSC_URL_DEV = "/";

it('Should login succesfully and show the welcome message', () => { 
    // Visita al portal
    cy.visit(WSC_URL_DEV)
    
    // Verificando existencia del titulo en el home
    cy.get('.page-title').should('have.text', 'Bienvenido a WSC')

})
 

})