//TOAST
//es un componente que sirve para enviar un mensaje, por ejemplo, 
//cuando hay un error en el login, manda el mensaje y despues se cierra
//este componente tiene 2 valores, uno maneja la interfaz grafica y el otro para saber si esta abierto o cerrado
const initialState = {
    open : false,
    mensaje : ""
};

const openSnackbarReducer = (state = initialState, action) => {
    switch(action.type) {
        case "OPEN_SNACKBAR" :
            return {
                ...state,
                open : action.openMensaje.open,
                mensaje : action.openMensaje.mensaje
            }
        default : 
            return state;
            
    }
}

export default openSnackbarReducer;