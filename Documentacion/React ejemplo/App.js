import React, {useState} from 'react';
import Perfil from './componentes/Perfil';

function App() {
  //variable de estado que puede ser consumida en todo el DOM
  //{nombre}
  const [nombre, cambiarNombre] = useState('No tiene nombre');

  //fucion que se dispara cuando el usuario escribe algo en la caja de texto
  // y es llamado mediante esta etiqueta -> onChange={eventoCajaTexto}
  function eventoCajaTexto(e){
    //e = el objeto
    //value = el valor (etiqueta)
    cambiarNombre(e.target.value);
  }

  return (
    <div>
      <h1>Cursos {nombre}</h1>
      <input name="nombre" type="text" value={nombre} onChange={eventoCajaTexto}/>
      <Perfil />
      
    </div>
  );
}

export default App;
