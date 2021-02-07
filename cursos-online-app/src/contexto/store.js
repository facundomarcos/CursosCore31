//para traer y para exportar data del initialState
import React, { createContext, useContext, useReducer } from 'react';

//provider - crea un proceso de suscripcion de los componentes react
//  y consumer - distribuye la data
//son las 2 propiedades que tiene el context para ingresar y distribuir data en el Storage
export const StateContext = createContext();

//reducer - cambiar un valor global a traves de un reducer
//initialState - el que almacena las variables globales
//children - los componentes react
export const StateProvider = ({reducer, initialState, children}) => (
    <StateContext.Provider value = {useReducer(reducer, initialState)}>
        {children}
    </StateContext.Provider>
);

//el useContext es un consumer
export const useStateValue = () => useContext(StateContext);

//reducer - 