import Reac, { useEffect, useState } from 'react';
import axios from 'axios';

function Perfil(props){
    //variable de estado, inicializa con un arreglo vacio
const [paises, obtenerPaises] = useState([]);
//evalua si la pagina se termino de cargar
const [status, cambiarStatus] = useState(false);

//cada vez que cambie el props, hace que se actualice el titulo de la pagina
useEffect( () => {
    

    axios.get('https://restcountries.eu/rest/v2/all')
    .then( resultado => {
         //inserta la data de paises
        obtenerPaises(resultado.data);
        //cambiar estado cuando se haya cargado
        cambiarStatus(true);
    })
    .catch(error => {
        cambiarStatus(true);
    })
    //evalua la propiedad y luego ejecuta lo de arriba
    //si los corchetes estan vacios, solo lo ejecuta una vez
}, []) 
/*
return (
    //el estilo se indica entre 2 llaves
    //y no es el mismo formato que html
    <div style={{background:"yellow"}}>
        Este es un nuevo componente {props.atributomio}
    </div>
);    */
//si cargaron los paises que los imprima en la lista
if(status){
    return (
        <ul>
            {paises.map((pais, indice) => 
                <li key={indice}> {pais.name} </li>
            )}
        </ul>
    );
    //sino, que ponga un cartel que diga cargando
}else{
    return (
        <h1>Cargando...</h1>
    );
    
}



}

export default Perfil;