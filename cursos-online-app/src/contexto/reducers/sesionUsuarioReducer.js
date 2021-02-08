//se encarga de almacenar la data del usuario que esta en sesion
//valores
//logica
//exportar funcion

//definicion de la data que se va a guardar posteriormente
export const initialState = {
    usuario : {
        nombreCompleto : '',
        email : '',
        username : '',
        foto : ''
    },
    autenticado : false
}

//
const sesionUsuarioReducer = (state = initialState, action) => {

        switch(action.type) {
            case "INICIAR_SESION" :
            return {
                ...state, 
                usuario : action.sesion,
                autenticado : action.autenticado
            };
            case "SALIR_SESION":
                return{
                    ...state,
                usuario : action.nuevoUsuario,
                autenticado : action.autenticado
                };
            case "ACTUALIZAR_USUARIO":
                return {
                    ...state,
                    usuario : action.nuevoUsuario,
                    autenticado : action.autenticado

                }
            default : return state;
                
        }
};

export default sesionUsuarioReducer;