import { Slider } from '../ui/slider';

interface Props {
  years: number[];
  setYears: (value: number[]) => void;
  min?: number;
}

function YearSlider({ years, setYears, min = 1930 }: Props) {
  return (
    <div className="">
      <div className="flex justify-between">
        <span>{years[0]}</span>
        <span>{years[1]}</span>
      </div>
      <Slider
      defaultValue={[1960,2000]}
        value={years}
        onValueChange={setYears}
        min={min}
        max={new Date().getFullYear()}
        step={1}
        className="mx-auto w-full max-w-xs"
      />
    </div>
  );
}

export default YearSlider;
